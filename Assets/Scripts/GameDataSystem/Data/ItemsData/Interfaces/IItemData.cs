using System.Collections.Generic;

public interface IItemData
{
    public int id { get; } // id предмета
    public string title { get; } // название предмета
    public string description { get; } // описание предмета
    public int price { get; } // цена предмета, за одну единицу
    public bool isBuyed { get; set; } // флаг покупки предмета
    public bool isEquipped { get; set; } // флаг экипирования предмета
    public bool isMultiplable { get; } // флаг возможности покупки нескольких предметов
    public int potential { get; } // геймплейная сила предмета в условных единицах
    public int itemCount { get; set; } // колличество купленого- предмета
}