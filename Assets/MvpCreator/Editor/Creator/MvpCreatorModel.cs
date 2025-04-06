namespace MvpCreator
{
    public class MvpCreatorModel
    {
        public bool CreateNewFolder { get; set; }
        public string Namespace { get; set; }
        public string FolderPath { get; set; }
        public string NewFolderName { get; set; }

        public string GetManagerTemplate()
        {
            return
    $@"using UnityEngine;
using MvpCreator;

namespace {Namespace}
{{
    [RequireComponent(typeof(View), typeof(Bootstrap))]
    public class Manager : MonoBehaviour, IBootstrapable
    {{
        private Model _model;
        private View _view;
        private Presenter _presenter;

        public Presenter Presenter => _presenter;

        public void BootstrapInit()
        {{
            _model = new Model();

            ModelInit();

            _view = GetComponent<View>();
            _presenter = new Presenter(_model, _view);
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
    public class Model
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
    public class View : MonoBehaviour
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
    public class Presenter
    {{
        private Model _model;
        private View _view;

        public Presenter(Model model, View view)
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