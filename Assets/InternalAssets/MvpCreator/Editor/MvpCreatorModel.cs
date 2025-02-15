namespace MvpCreator
{
    public class MvpCreatorModel
    {
        public string ModuleName { get; set; }
        public string Namespace { get; set; }
        public string FolderPath { get; set; }

        public string GetManagerTemplate()
        {
            return
    $@"using UnityEngine;
using MvpCreator;

namespace {Namespace}
{{
    [RequireComponent(typeof({ModuleName}View), typeof(Bootstrap))]
    public class {ModuleName}Manager : MonoBehaviour, IBootstrapable
    {{
        private {ModuleName}Model _model;
        private {ModuleName}View _view;
        private {ModuleName}Presenter _presenter;

        public {ModuleName}Presenter Presenter => _presenter;

        public void BootstrapInit()
        {{
            _model = new {ModuleName}Model();

            ModelInit();

            _view = GetComponent<{ModuleName}View>();
            _presenter = new {ModuleName}Presenter(_model, _view);
        }}

        private void ModelInit()
        {{
            // Manager model initialization here
        }}

        // Manager logic here
    }}
}}";
        }

        public string GetModelTemplate()
        {
            return
    $@"using UnityEngine;

namespace {Namespace}
{{
    public class {ModuleName}Model
    {{
        // Model properties and logic here
    }}
}}";
        }

        public string GetViewTemplate()
        {
            return
    $@"using UnityEngine;

namespace {Namespace}
{{
    public class {ModuleName}View : MonoBehaviour
    {{
        // View components and UI handling here
    }}
}}";
        }

        public string GetPresenterTemplate()
        {
            return
    $@"using UnityEngine;

namespace {Namespace}
{{
    public class {ModuleName}Presenter
    {{
        private {ModuleName}Model _model;
        private {ModuleName}View _view;

        public {ModuleName}Presenter({ModuleName}Model model, {ModuleName}View view)
        {{
            _model = model;
            _view = view;
        }}

        // Presenter logic here
    }}
}}";
        }
    }
}