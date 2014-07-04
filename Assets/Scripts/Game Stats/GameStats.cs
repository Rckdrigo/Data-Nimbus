using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class GameStats : MonoBehaviour {
	
	public TextAsset xml;
	List<Dictionary<string,string>> levels = new List<Dictionary<string,string>>();
	Dictionary<string,string> level;
	
	public void gameLoad(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
 		xmlDoc.LoadXml(xml.text); // load the file.
	
		XmlNodeList levelsList = xmlDoc.GetElementsByTagName("level");
		foreach (XmlNode levelInfo in levelsList){
	   		XmlNodeList levelcontent = levelInfo.ChildNodes;
	   		level = new Dictionary<string,string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.
	
			if(levelInfo.Attributes["active"].Value == "true"){
				foreach (XmlNode levelsStats in levelcontent){ // levels itens nodes
					
					if(levelsStats.Name == "world")
						level.Add("world",levelsStats.InnerXml); // put this in the dictionary.
					if(levelsStats.Name == "name")
						level.Add("name",levelsStats.InnerXml); // put this in the dictionary.
					if(levelsStats.Name == "score")
						level.Add("score",levelsStats.InnerXml); // put this in the dictionary.
					if(levelsStats.Name == "stars")
						level.Add("stars",levelsStats.InnerXml); // put this in the dictionary.
					
				}
				levels.Add(level);
			}
		}
	}
	
	//Leyendo el archivo
	
	public int getLvlWorld(int lvl){
		string world = "";
			levels[lvl].TryGetValue("world",out world);
		return int.Parse(world);
	}
	
	public string getLvlName(int lvl){
		string name = "";
			levels[lvl].TryGetValue("name",out name);
		return name;
	}
	
	public int getLvlStars(int lvl){
		string stars = "";
			levels[lvl].TryGetValue("stars",out stars);
		return int.Parse(stars);
	}
	
	public int getLvlScore(int lvl){
		string score = "";
			levels[lvl].TryGetValue("score",out score);
		return int.Parse(score);
	}
	
	public List<Dictionary<string,string>> getLvlList(){
		return levels;
	}
	
}
