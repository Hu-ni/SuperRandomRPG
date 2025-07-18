using SuperRandomRPG.Models;
using System;
using Team_SRRPG.Model;

public static class CharacterCreator
{
    public static Player Create()
	{
		Console.WriteLine("캐릭터를 생성합니다");
        Console.WriteLine();
        Console.WriteLine("당신의 이름을 입력하세요");
        string name = Console.ReadLine();

        Job job;

        Console.Clear();

        while (true)
        {
            Console.WriteLine("당신의 직업은 무엇입니까?");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 궁사");
            Console.WriteLine("4. 도박꾼");

            string input = Console.ReadLine();

            if (input == "1")
            {
                job = Job.Warrior;
                break;
            }
            else if (input == "2")
            {
                job = Job.Mage;
                break;
            }
            else if (input == "3")
            {
                job = Job.Archer;
                break;
            }
            else if (input == "4")
            {
                job = Job.Gambler;
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("다시 입력해주세요.");
            }
        }
        Player player = new Player(name, job);

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("캐릭터가 생성되었습니다.");
        return player;
    }
}
