using System;
using Team_SRRPG.Model;

public static class CharacterCreator
{
	public static CharacterCreator()
	{
		Console.WriteLine("캐릭터를 생성합니다");
        Console.WriteLine();
        Console.WriteLine("당신의 이름을 입력하세요");
        string name = Console.ReadLine();

        Console.WriteLine("당신의 직업은 무엇입니까?");
        Console.WriteLine("1. Warrior");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Archer");
        int jobNumber = int.Parse(Console.ReadLine());
        Job job;

        if (jobNumber == 1)
        {
            job = Job.Warrior;
        }
        else if (jobNumber == 2)
        {
            job = Job.Mage;
        }
        else
        {
            job = Job.Archer;
        }

        Console.WriteLine();
        Console.WriteLine("캐릭터가 생성되었습니다.");
    }
}
