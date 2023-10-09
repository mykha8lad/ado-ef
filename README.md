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
