using System;
 

[Serializable]
public class FishData // класс содержащий информацию о каждой рыбе
{
    public int id; // индетефикатор 
    public string title; // название 
    public string description; // описание 
    public int rewardMoney; // максимальная награда за поимку
    public int level; //уровень с которого можно поимать рыбу
    public int catchStartHour; // первый час ловли
    public int catchEndHour; // последний час ловли
    public bool isCathed; //была ли рыба поймана
    public char bestRank; // лучший ранк пойманой рыбы
    public int recordReward; //самая большая цена рыбы
}