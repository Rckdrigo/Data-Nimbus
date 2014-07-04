using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

/// <summary>
/// XML serializer. Esta clase se dedicara a serializar las clases que se le indique y las guardara en un formato de .xml.
/// Es necesario establecer en la clase cuales variables seran consideradas como atributos o como etiquetas.
/// </summary>

public class XMLManager{
	
	/* The following metods came from the referenced URL */ 
	public static string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 
    
	public static byte[] StringToUTF8ByteArray(string pXmlString){ 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 
    
   // Here we serialize our UserData object of myData 
    public static string SerializeObject(object pObject, string typeName)
    {
        System.Type type = System.Type.GetType(typeName);
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(type);
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xmlTextWriter.Formatting = Formatting.Indented;
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }  
    
   // Here we deserialize it back into its original form 
	public static T DeserializeObject<T>(string pXmlizedString){ 
		XmlSerializer xs = new XmlSerializer(typeof(T)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		//      XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return (T)xs.Deserialize(memoryStream); 
	} 
    
   
	
	public static void CreateXML(string _FileName,string _data){ 
		string _FileLocation;
		if(Application.platform == RuntimePlatform.Android)
			_FileLocation = Application.persistentDataPath;
		else
			_FileLocation = Application.dataPath;
		
		StreamWriter writer;
		FileInfo t = new FileInfo(_FileLocation+"/"+ _FileName); 
		if(!t.Exists) 
			writer = t.CreateText(); 
		else { 
			t.Delete(); 
			writer = t.CreateText(); 
		} 
		writer.Write(_data); 
		writer.Close(); 
	} 
	
	public static string LoadXML(string _FileName ){ 
		string _FileLocation;
		if(Application.platform == RuntimePlatform.Android)
			_FileLocation = Application.persistentDataPath;
		else
			_FileLocation = Application.dataPath;
		StreamReader r;
		
		r = File.OpenText(_FileLocation+"/"+ _FileName); 
		string _info = r.ReadToEnd();
		r.Close(); 
		return _info;
	} 

}
