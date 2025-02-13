# How to use
The utility creates an MVP module in the selected directory. To do this, you need to:
1. Right-click -> Create -> "MVP Creator";
2. Enter the name of the MVP module;
3. If necessary, change the module creation path;
4. Click "Generate MVP Module".

A folder with 4 files should be generated: {BaseName}Model, {BaseName}View, {BaseName}Presenter, {BaseName}Manager.

To add the module to the object, only the Manager needs to be added.

# MVP Creator
Responsible for creating the MVP module.

Creates 4 files:
- Manager - initializes other members of the module. It is the entry point for data transfer from the inspector and the access point to the Presenter by other modules. Inherits from IBootstrapable, which allows initializing the module in a certain order;
- Model - stores data necessary for the module's operation;
- View - inputs and outputs data. Inherits from MonoBehaviour, which allows access to its functionality from the Presenter;
- Presenter - module logic. Has access to View and Model for operation. Does not have access to Manager.

# Bootstrap
Responsible for the sequential initialization of MVP modules.

When adding a module to an object, Bootstrap is automatically added.

It provides an array where IBootstrapable objects can be added, thus setting the order of initialization by priority starting from 0.

Bootstrap itself is IBootstrapable, which allows setting the initialization order not only of components but also of the objects on which it resides.

# Additional
If the Presenter needs MonoBehaviour methods such as Awake, Start, Update, etc., it is possible to add an `event` for their handling.

**View**
```csharp
public class View : MonoBehaviour
{
    public event Action OnFixedUpdate;

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
```

**Presenter**
```csharp
public class Presenter
{
    private Model _model;
    private View _view;

    public Presenter(Model model, View view)
    {
        _model = model;
        _view = view;
    }

    private void Start()
    {
        _view.OnFixedUpdate += OnFixedUpdate;
    }

    private void OnFixedUpdate()
    {
        // ...
    }
}
```
