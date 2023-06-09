using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class FileDataNode
    {
        public int ID;
        public string Title;
        public string Content;
    }

    public class FileDataList
    {
        public List<FileDataNode> Data;
    }
}
