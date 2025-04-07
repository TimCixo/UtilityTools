namespace MvpCreator
{
    public class MvpCreatorModel
    {
        public bool CreateNewFolder { get; set; }
        public string Namespace { get; set; }
        public string Prefix { get; set; }
        public string FolderPath { get; set; }
        public string NewFolderName { get; set; }

        public string GetManagerTemplate()
        {
            return
    $@"using UnityEngine;
using MvpCreator;

namespace {Namespace}
{{
    [RequireComponent(typeof({Prefix}View), typeof(Bootstrap))]
    public class {Prefix}Manager : MonoBehaviour, IBootstrapable
    {{
        private {Prefix}Model _model;
        private {Prefix}View _view;
        private {Prefix}Presenter _presenter;

        public {Prefix}Presenter Presenter => _presenter;

        public void BootstrapInit()
        {{
            _model = new {Prefix}Model();

            ModelInit();

            _view = GetComponent<{Prefix}View>();
            _presenter = new {Prefix}Presenter(_model, _view);
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
    public class {Prefix}Model
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
    public class {Prefix}View : MonoBehaviour
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
    public class {Prefix}Presenter
    {{
        private {Prefix}Model _model;
        private {Prefix}View _view;

        public {Prefix}Presenter({Prefix}Model model, {Prefix}View view)
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