using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Xml;


public class XMLFile
{
    public System.Xml.XmlDocument doc;
    System.Xml.XmlNode node;

    public XMLFile()
    {
        doc = new System.Xml.XmlDocument();
        node = doc.CreateNode(System.Xml.XmlNodeType.Element, "root", null);

        doc.AppendChild(node);
    }

    public bool Load(Stream file)
    {
        try
        {
            doc = new System.Xml.XmlDocument();
            doc.Load(file);

            return true;
        }
        catch (System.Exception)
        {
            
        }

        return false;
    }

    public bool Save(String filename)
    {
        try
        {
            doc.Save(filename);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Save(Stream stream)
    {
        try
        {
            doc.Save(stream);
            //stream.Close();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public XmlElement Root
    {
        get
        {
            if (doc != null)
            {
                return doc.DocumentElement;
            }

            return null;
        }
    }

    //var node = file.doc.CreateNode(System.Xml.XmlNodeType.Element, "collider", null);
    public XmlNode CreateNode(String label)
    {
        if(doc != null)
        {
            return doc.CreateNode(System.Xml.XmlNodeType.Element, label, null);
        }

        return null;
    }

    

    public void AddAttribute(System.Xml.XmlNode node, String label, String value)
    {
        System.Xml.XmlAttribute attrib;
        attrib = doc.CreateAttribute(label);
        attrib.Value = value;
        node.Attributes.Append(attrib);
    }
}

public class XMLLoadFile : XMLFile
{
    public XMLLoadFile(Stream file)
    {
        doc = new System.Xml.XmlDocument();
        doc.Load(file);
    }
}


public class XMLSaveFile : XMLFile
{
    System.Xml.XmlNode node;

    public XMLSaveFile()
    {
        doc = new System.Xml.XmlDocument();
        node = doc.CreateNode(System.Xml.XmlNodeType.Element, "root", null);

        doc.AppendChild(node);
    }

    public bool Write(Stream stream)
    {
        try
        {
            doc.Save(stream);
            stream.Close();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
