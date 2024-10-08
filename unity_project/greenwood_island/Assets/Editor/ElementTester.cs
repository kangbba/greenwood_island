using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening;

public class ElementTester : EditorWindow
{
    private Vector2 _scrollPositionLeft; // 왼쪽 스크롤 위치
    private Vector2 _scrollPositionRight; // 오른쪽 스크롤 위치
    private List<Type> _elementTypes; // 모든 Element 타입 리스트
    private List<Type> _filteredElementTypes; // 필터된 Element 리스트 (검색 및 정렬 반영)
    private Type _selectedElementType; // 현재 선택된 Element 타입
    private List<object> _parameterValues; // 생성자 파라미터 값 저장 리스트
    private ConstructorInfo _selectedConstructor; // 선택된 생성자
    private string _searchQuery = ""; // 검색어 저장
    private bool _isAlphabeticalOrder = true; // ABC 정렬 여부

    [MenuItem("GreenwoodIsland/Element Tester")]
    public static void ShowWindow()
    {
        ElementTester window = GetWindow<ElementTester>("Element Tester");
        window.minSize = new Vector2(1000, 700);
        window.Show();
    }

    private void OnEnable()
    {
        // 모든 Element 타입을 찾아 리스트에 저장
        _elementTypes = Assembly.GetAssembly(typeof(Element))
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Element)) && !t.IsAbstract)
            .ToList();

        _filteredElementTypes = new List<Type>(_elementTypes);
        SortElements(); // 초기 정렬
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Element Tester", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 검색 및 정렬 옵션
        DrawSearchAndSortOptions();

        EditorGUILayout.BeginHorizontal();

        // 왼쪽 영역: Element 버튼 리스트
        DrawElementButtons();

        // 오른쪽 영역: 선택된 Element의 필드와 테스트 실행
        DrawSelectedElementDetails();

        EditorGUILayout.EndHorizontal();
    }

    private void DrawSearchAndSortOptions()
    {
        EditorGUILayout.BeginHorizontal();

        // ABC 정렬 버튼
        if (GUILayout.Button("ABC 순으로 정렬", GUILayout.Width(120)))
        {
            _isAlphabeticalOrder = true;
            SortElements();
        }

        // 카테고리별 정렬 버튼
        if (GUILayout.Button("카테고리별 보기", GUILayout.Width(120)))
        {
            _isAlphabeticalOrder = false;
            SortElements();
        }

        EditorGUILayout.EndHorizontal();

        // 검색 입력 필드
        string newSearchQuery = EditorGUILayout.TextField("", _searchQuery, GUILayout.Width(240));
        if (newSearchQuery != _searchQuery) // 검색어가 변경되었을 때 자동으로 검색
        {
            _searchQuery = newSearchQuery;
            FilterElements();
        }

        EditorGUILayout.Space();
    }

    private void SortElements()
    {
        if (_isAlphabeticalOrder)
        {
            _filteredElementTypes = _filteredElementTypes.OrderBy(t => t.Name).ToList();
        }
        else
        {
            // 카테고리별 정렬 (비슷한 이름끼리 그룹화)
            _filteredElementTypes = _filteredElementTypes
                .OrderBy(t => GetCategoryName(t.Name)) // 카테고리 이름을 기준으로 정렬
                .ThenBy(t => t.Name)
                .ToList();
        }
    }

    private string GetCategoryName(string elementName)
    {
        // 카테고리를 지정하는 규칙 설정
        if (elementName.Contains("Film")) return "필름";
        if (elementName.Contains("CutIn")) return "컷인";
        if (elementName.Contains("Camera")) return "카메라";
        if (elementName.Contains("Choice")) return "초이스";
        if (elementName.Contains("Character")) return "캐릭터";
        if (elementName.Contains("SFX")) return "사운드";
        if (elementName.Contains("FX")) return "비주얼 이펙트";
        if (elementName.Contains("Place")) return "장소";
        if (elementName.Contains("Dialogue")) return "대화";
        // 기본 카테고리
        return "General";
    }

    private void FilterElements()
    {
        // 검색어에 따라 필터링, 검색어가 비어있으면 전체 표시
        _filteredElementTypes = string.IsNullOrEmpty(_searchQuery)
            ? new List<Type>(_elementTypes)
            : _elementTypes.Where(t => t.Name.ToLower().Contains(_searchQuery.ToLower())).ToList();

        SortElements();
    }

    private void DrawElementButtons()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.35f)); // 좌측 패널의 너비를 조정
        _scrollPositionLeft = EditorGUILayout.BeginScrollView(_scrollPositionLeft);

        string lastHeader = ""; // 마지막에 출력된 헤더를 추적하여 중복 방지

        foreach (var elementType in _filteredElementTypes)
        {
            string currentHeader = _isAlphabeticalOrder ? elementType.Name[0].ToString().ToUpper() : GetCategoryName(elementType.Name);

            // 헤더가 변경될 때만 출력
            if (currentHeader != lastHeader)
            {
                EditorGUILayout.LabelField(currentHeader, EditorStyles.boldLabel);
                lastHeader = currentHeader;
            }

            // 더 긴 버튼 높이 설정
            if (GUILayout.Button(elementType.Name, GUILayout.Height(40)))
            {
                _selectedElementType = elementType;
                _selectedConstructor = _selectedElementType.GetConstructors().FirstOrDefault(); // 첫 번째 생성자를 선택
                InitializeParameterValues();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void InitializeParameterValues()
    {
        if (_selectedConstructor == null)
            return;

        // 생성자의 파라미터 값을 초기화
        _parameterValues = new List<object>();
        foreach (var param in _selectedConstructor.GetParameters())
        {
            _parameterValues.Add(GetDefaultValue(param.ParameterType));
        }
    }

    private object GetDefaultValue(Type type)
    {
        // 기본값 생성, float의 경우 1로 설정
        if (type == typeof(float))
            return 1f;
        if (type.IsValueType)
            return Activator.CreateInstance(type);
        return null;
    }

    private void DrawSelectedElementDetails()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.65f)); // 우측 패널의 너비를 조정
        _scrollPositionRight = EditorGUILayout.BeginScrollView(_scrollPositionRight);

        if (_selectedElementType != null && _selectedConstructor != null)
        {
            EditorGUILayout.LabelField($"Testing: {_selectedElementType.Name}", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);

            // 생성자 파라미터 시각화 및 편집
            var parameters = _selectedConstructor.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                _parameterValues[i] = DrawParameterField(param, _parameterValues[i]);
                EditorGUILayout.Space(5); // 각 필드 간 공간 추가
            }

            EditorGUILayout.Space(20);

            // "테스트하기" 버튼
            if (GUILayout.Button("테스트하기", GUILayout.Height(40)))
            {
                TestSelectedElement();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("테스트할 Element를 선택하세요.", MessageType.Info);
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private object DrawParameterField(ParameterInfo param, object currentValue)
    {
        // 파라미터의 타입에 따라 적절한 입력 필드 제공
        if (param.ParameterType == typeof(float))
        {
            return EditorGUILayout.FloatField(param.Name, currentValue != null ? (float)currentValue : 1f);
        }
        if (param.ParameterType == typeof(int))
        {
            return EditorGUILayout.IntField(param.Name, currentValue != null ? (int)currentValue : 0);
        }
        if (param.ParameterType == typeof(string))
        {
            return EditorGUILayout.TextField(param.Name, currentValue != null ? (string)currentValue : string.Empty);
        }
        if (param.ParameterType == typeof(bool)) // bool 타입 처리 추가
        {
            return EditorGUILayout.Toggle(param.Name, currentValue != null ? (bool)currentValue : false);
        }
        if (param.ParameterType == typeof(Color))
        {
            return EditorGUILayout.ColorField(param.Name, currentValue != null ? (Color)currentValue : Color.white);
        }
        if (param.ParameterType == typeof(Ease))
        {
            return (Ease)EditorGUILayout.EnumPopup(param.Name, currentValue != null ? (Ease)currentValue : Ease.OutQuad);
        }
        if (param.ParameterType == typeof(Vector2))
        {
            return EditorGUILayout.Vector2Field(param.Name, currentValue != null ? (Vector2)currentValue : Vector2.zero);
        }
        if (param.ParameterType == typeof(Sprite))
        {
            return (Sprite)EditorGUILayout.ObjectField(param.Name, (Sprite)currentValue, typeof(Sprite), false);
        }
        if (param.ParameterType.IsEnum)
        {
            return EditorGUILayout.EnumPopup(param.Name, (Enum)currentValue);
        }

        EditorGUILayout.LabelField($"{param.Name} (Unsupported Type: {param.ParameterType.Name})");
        return currentValue;
    }

    private void TestSelectedElement()
    {
        // 플레이 중인지 확인
        if (!Application.isPlaying)
        {
            EditorUtility.DisplayDialog("알림", "플레이 중에만 테스트할 수 있습니다.", "확인");
            return;
        }

        try
        {
            // 파라미터 값을 사용하여 선택된 Element 인스턴스 생성
            Element elementInstance = (Element)_selectedConstructor.Invoke(_parameterValues.ToArray());
            elementInstance.Execute();
            Debug.Log($"Executed Element: {_selectedElementType.Name}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error executing {_selectedElementType.Name}: {ex.Message}");
        }
    }
}
