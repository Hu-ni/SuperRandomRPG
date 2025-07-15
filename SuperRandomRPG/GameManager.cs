using Spartdungeon.Services;
using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG
{
    public class GameManager
    {
        private Player _player;
        private List<Dungeon> _dungeons;
        private Inventory _inventory;

        private bool playerDataExists = false;
        public void Initialize()
        {
            if (File.Exists("\\Data\\Player.xml"))   //실행 위치(bin/(Debug or Release)/net8.0)에 /Data/Player.xml 파일이 있는지 검사
            {
                _player = XmlSerializerHelper.Deserialize<Player>("\\Data\\Player.xml");    //있을 경우 데이터 가져오기
                playerDataExists = true;
            }
            else
                
            _dungeons = new List<Dungeon>();
            _inventory = new Inventory();
        }

        //시작 함수
        public void Run()
        {
            Console.Clear();

            if (playerDataExists)    //세이브 데이터가 없을 경우
                ;// 플레이어 생성 처리 로직


            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                switch(input)
                {
                    case 1:
                        //1번 화면 생성
                        break;
                    case 2:
                        //2번 화면 생성
                        break;
                    case 3:
                        //3번 화면 생성
                        break;
                    case 4:
                        //4번 화면 생성
                        break;
                    case 5:
                        //5번 화면 생성
                        break;
                }
            }
        }
    }
}
