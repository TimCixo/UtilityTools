public class MvpCreatorModel
{
    private string _baseName;

    public MvpCreatorModel(string baseName)
    {
        _baseName = baseName;
    }

    public string GetModelTemplate()
    {
        return
$@"using UnityEngine;

public class {_baseName}Model
{{
    // Model properties and logic here
}}";
    }

    public string GetViewTemplate()
    {
        return
$@"using UnityEngine;

public class {_baseName}View : MonoBehaviour
{{
    // View components and UI handling here
}}";
    }

    public string GetPresenterTemplate()
    {
        return
$@"using UnityEngine;

public class {_baseName}Presenter
{{
    private {_baseName}Model _model;
    private {_baseName}View _view;

    public {_baseName}Presenter({_baseName}Model model, {_baseName}View view)
    {{
        _model = model;
        _view = view;
    }}

    // Presenter logic here
}}";
    }

    public string GetManagerTemplate()
    {
        return
$@"using UnityEngine;

[RequireComponent(typeof({_baseName}View), typeof(Bootstrap))]
public class {_baseName}Manager : MonoBehaviour, IBootstrapable
{{
    private {_baseName}Model _model;
    private {_baseName}View _view;
    private {_baseName}Presenter _presenter;

    public {_baseName}Presenter Presenter => _presenter;

    public void BootstrapInit()
    {{
        _model = new {_baseName}Model();

        ModelInit();

        _view = GetComponent<{_baseName}View>();
        _presenter = new {_baseName}Presenter(_model, _view);
    }}

    private void ModelInit()
    {{
        // Manager model initialization here
    }}

    // Manager logic here
}}";
    }
}