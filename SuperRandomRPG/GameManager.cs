using Spartdungeon.Services;
using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG
{
    public class GameManager
    {
        private Player _player;
        private DungeonManager _dungeonManager;
        private Inventory _inventory;
        private Shop _shop;

        private bool playerDataExists = false;

        private bool StartMenu()
        {
            Random rand = new Random();

            Console.WriteLine("세상은 확률로 돌아간다.");
            Console.WriteLine("확률을 지배해 던전을 클리어하자.");
            Console.WriteLine();
            Console.WriteLine("1.시작하기 (50%)");
            Console.WriteLine("2.종료하기 (50%)");

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    if (rand.NextDouble() < 0.5)
                    {
                        Console.Clear();
                        Console.WriteLine("게임 시작");
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("시작 실패, 게임 종료");
                        return false;
                    }
                }
                else if (input == "2")
                {
                    if (rand.NextDouble() < 0.5)
                    {
                        Console.Clear();
                        Console.WriteLine("게임 종료");
                        return false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("종료 실패, 게임 시작");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }
        }

        public void Initialize()
        {
            if (File.Exists("\\Data\\Player.xml"))   //실행 위치(bin/(Debug or Release)/net8.0)에 /Data/Player.xml 파일이 있는지 검사
            {
                // Save 파일로 저장하기 위해 만든 데이터 클레스
                SaveFileDTO dto = XmlSerializerHelper.Deserialize<SaveFileDTO>("\\Data\\Player.xml");    //있을 경우 데이터 가져오기
                _player = dto.Player;
                _inventory = dto.Inventory;
                
                playerDataExists = true;
            }
            else
            {
                _inventory = new Inventory();
            }

            if (_player != null)
            {
                _dungeonManager = new DungeonManager(_player);
            }
            _shop = new Shop(_inventory);
            
        }

        //시작 함수
        public void Run()
        {
            if (!StartMenu())
            {
                Console.WriteLine("게임 종료");
                return;
            }

            Console.Clear();

            if (playerDataExists == false)    //세이브 데이터가 없을 경우
            {
                _player = CharacterCreator.Create();
                _dungeonManager = new DungeonManager(_player);
                //Save();
            } // 플레이어 생성 처리 로직

            //Village();  //마을 가는 로직

            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        // 1번 화면 생성  
                        _player.OpenStatus(_inventory); // 플레이어 상태창 출력  
                        break;
                    case 2:
                        // 2번 화면 생성  
                        break;
                    case 3:
                        _inventory.OpenInventory();
                        // 3번 화면 생성  
                        break;
                    case 4:
                        _shop.OpenShop(_player);
                        // 4번 화면 생성  
                        break;
                    case 5:
                        _dungeonManager.ShowDungeonSelectionScene();
                        break;
                    
                }

                //Save();     //플레이어 데이터 저장
            }
        }


        /// <summary>
        /// 인벤토리와 플레이어 데이터를 하나의 클레스로 보관하기 위해
        /// SaveFIleDTO 클레스를 생성해 파일로 저장.
        /// </summary>
        public void Save()
        {
            XmlSerializerHelper.Serialize(new SaveFileDTO { Player = _player, Inventory = _inventory},
                        "\\Data\\Player.xml");
        }
    }
}
