# How to use
Утиліта створює MVP модуль у вибраній директорії. Для цього потрібно:
1. Вибрати ПКМ -> Create -> "MVP Creator";
2. Ввести назву MVP модуля;
3. При необхідності замінити шлях створення модуля;
4. Натиснути "Genetate MVP Module".

Має згенеруватися папка з 4 файлами: {BaseName}Model, {BaseName}View, {BaseName}Presenter, {BaseName}Manager.

Щоб додати модуль на об'єкт - необхідно додати лише Manager. 
# MVP Creator
Відповідає за створення MVP модуля.

Створює 4 файли:
- Manager - ініціалізація інших членів модуля. Є вхідною точкою для передачі даних з інспектора і точкою доступу до Presenter іншими модулями. Успадковується від IBootstrapable, що дозволяє ініціалізувати модуль в певній черзі;
- Model - збереження дані необхідних для роботи модуля;
- View - введення і виведення даних. Успадковується від MonoBehaviour, що дозволяє отримати доступ до його функціоналу з Presenter;
- Presenter - логіка модуля. Має доступ до View та Model для роботи. Не має доступу до Manager.
# Bootstrap
Відповідає за послідовну ініціалізацію MVP модулей.

При додаванні модуля на об'єкт автоматично додається Bootstrap. 

Він надає масив, куди можна додати IBootstrapable об'єкти тим самим встановивши порядок ініціалізації за приорітетністтю починаючи з 0.

Сам Bootstrap є IBootstrapable, що дозволяє задавати порядок ініціалізації не тільки компонентів, а і об'єктів, на яких він знаходиться.
# Additional
Якщо для Presenter необхідні методи MonoBehaviour такі як Awake, Start, Update тощо, можливо додати `event` для їх обробки. 

**View**
```c-sharp
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
```c-sharp
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