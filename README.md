## Home work 29.08
> * Д.З. По БД, створеної на зустрічі, вивести (у "моніторі БД") засобами LINQ
> * Кількість підлеглих (осіб, що мають керівника)
> * Кількість співробітників ІТ-відділу (як основних, так і сумісників)
> * Кількість співробітників, які працюють у двох відділах (основний та сумісний)

[![photo-2023-10-04-22-29-47.jpg](https://i.postimg.cc/MKffJw3K/photo-2023-10-04-22-29-47.jpg)](https://postimg.cc/Mvxp1C8h)
___
## Home work 31.08
> * Д.З. Використовуючи LINQ-to-Entity та створену БД
> * Вивести дані
> * Прізвище І.Б. співробітника  --  Назва відділу (за сумісництвом: SecDep)
> * Впорядкувати по назві відділу

```cs
private void Button5_Click(object sender, RoutedEventArgs e)
{
    var query = dataContext
        .Managers
        .Join(
            dataContext.Departments,
            m => m.IdSecDep,
            d => d.Id,
            (m, d) => new Pair() { Key = $"{m.Surname} {m.Name[0]}.{m.Secname[0]}.", Value = d.Name }
        )
        .Take(7)
        .OrderBy(pair => pair.Value);

    Pairs.Clear();
    foreach (var pair in query)
    {
        Pairs.Add(pair);
    }
}
```
[![photo-2023-10-09-14-57-33.jpg](https://i.postimg.cc/qMYFXBnF/photo-2023-10-09-14-57-33.jpg)](https://postimg.cc/gnV4GWgy)
___
## Home work 01.09
> * Д.З. Засобами LINQ на основі створеної БД реалізувати запити
> * Назва відділу -- кількість сумісників (SecDep)
> * Запит з однофамільцями переробити з нумерацією
>    1. Андріяш
>    2. Лешків
> * Вивести трьох співробітників з найбільшою кількістю підлеглих (к-сть підлеглих --- П.І.Б.)

[![photo-2023-10-09-17-59-21.jpg](https://i.postimg.cc/Fs377rSN/photo-2023-10-09-17-59-21.jpg)](https://postimg.cc/KRcZWyQH)
[![photo-2023-10-09-17-59-47.jpg](https://i.postimg.cc/wTXj9WKB/photo-2023-10-09-17-59-47.jpg)](https://postimg.cc/9rfhpYQ3)
[![photo-2023-10-09-18-01-25.jpg](https://i.postimg.cc/4yzsN3S0/photo-2023-10-09-18-01-25.jpg)](https://postimg.cc/gLkfgGdy)
___
## Home work 05.09
> * Д.З. У власних проєктах додати до сутностей навігаційні властивості
> * Налаштувати їх відповідним чином
> * Реалізувати зв'язки даних через ці елементи

### Поки не взявся за власний проект, створив сутність із навігаційними властивостями у проекті класної роботи
```cs
private void ButtonNav3_Click(object sender, RoutedEventArgs e)
{
    var query = dataContext
    .Managers
    .Include(m => m.MainDep)
    .Select(m => new Pair
    {
        Key = m.Id.ToString(),
        Value = m.IdSecDep == null ? "--" : (m.Surname + ' ' + m.Name)
    });

    Pairs.Clear();
    foreach (var pair in query)
    {
        Pairs.Add(pair);
    }
}
```
[![photo-2023-10-15-15-11-30.jpg](https://i.postimg.cc/VkVkXc4j/photo-2023-10-15-15-11-30.jpg)](https://postimg.cc/TKnxXFzw)
___

