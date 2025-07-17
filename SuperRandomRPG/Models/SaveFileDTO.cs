using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    /// <summary>
    /// 세이브 파일 저장을 위한 데이터 모델
    /// </summary>
    [XmlRoot("SaveData")]   //XML 구조를 명확하게 제시하기 위한 속성
    public class SaveFileDTO
    {
        [XmlElement("Player")]
        public Player Player { get; set; }

        [XmlElement("Inventory")]
        public Inventory Inventory { get; set; }

        [XmlElement("Shop")]
        public Inventory Shop { get; set; }
    }
}
