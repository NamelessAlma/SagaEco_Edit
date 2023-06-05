﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace COF_LAUNCH
{
    public unsafe class ECODatFile : Header
    {
        Dictionary<string, Header> headers = new Dictionary<string, Header>();

        System.IO.BinaryReader brdata;
        System.IO.BinaryWriter bwdata;
        System.IO.BinaryReader brhed;
        System.IO.BinaryWriter bwhed;

        public Dictionary<string, Header> Files { get { return this.headers; } }

        public void Open(string path)
        {
            if (System.IO.Path.GetExtension(path) != ".hed")
                throw new ArgumentException("Can only open header(.hed) file");
            headers.Clear();
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.IO.FileStream datafs = new System.IO.FileStream(System.IO.Path.GetDirectoryName(path) + "\\" + System.IO.Path.GetFileNameWithoutExtension(path) + ".dat", System.IO.FileMode.Open);
            brhed = new System.IO.BinaryReader(fs);
            bwhed = new System.IO.BinaryWriter(fs);
            brdata = new System.IO.BinaryReader(datafs);
            bwdata = new System.IO.BinaryWriter(datafs);
            int filelist = brhed.ReadInt32();
            int namepacklen = brhed.ReadInt32();
            int namelen = brhed.ReadInt32();
            byte[] names = new byte[namelen];
            if ((namepacklen & 0x80000000) == 0x80000000)
                namepacklen = (int)(namepacklen ^ 0x80000000);
            brdata.BaseStream.Position = filelist;
            if (namepacklen != namelen)
                Unpack(brdata.ReadBytes(namepacklen), names);
            else
                names = brdata.ReadBytes(namepacklen);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(names);
            ms.Position = 0;
            System.IO.BinaryReader brname = new System.IO.BinaryReader(ms);
            int count = brname.ReadInt32();
            string[] filenames = System.Text.Encoding.ASCII.GetString(brname.ReadBytes(namelen - 4)).Split('\0');
            for (int i = 0; i < count; i++)
            {
                if (filenames[i] == "")
                    continue;
                Header header = new Header();
                header.filename = filenames[i];
                header.offset = brhed.ReadInt32();
                header.packsize = brhed.ReadInt32();
                header.unpacksize = brhed.ReadInt32();
                header.index = i;
                headers.Add(filenames[i], header);
            }
        }

        public void Repack(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            string tgPath = System.IO.Path.GetDirectoryName(path);
            string tgPre = System.IO.Path.GetFileNameWithoutExtension(path);
            System.IO.FileStream output = new System.IO.FileStream(tgPath + "\\" + tgPre + ".dat", System.IO.FileMode.Create);
            System.IO.FileStream header = new System.IO.FileStream(tgPath + "\\" + tgPre + ".hed", System.IO.FileMode.Create);
            System.IO.BinaryWriter bwout = new System.IO.BinaryWriter(output);
            System.IO.BinaryWriter bwhed = new System.IO.BinaryWriter(header);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bwms = new System.IO.BinaryWriter(ms);
            bwms.Write(files.Length);
            foreach (string i in files)
            {
                //if (System.IO.Path.GetExtension(i) != ".csv")
                //    continue;
                string file = System.IO.Path.GetFileName(i) + "\0";
                byte[] buf = System.Text.Encoding.ASCII.GetBytes(file);
                bwms.Write(buf);
            }
            ms.Flush();
            byte[] filelist = new byte[(int)(ms.Position + 1)];
            Array.Copy(ms.GetBuffer(), filelist, filelist.Length);
            int listsize = filelist.Length;
            int listPackSize = filelist.Length;
                Pack(filelist, out filelist);
                listPackSize = (int)(Convert.ToUInt32(filelist.Length) | 0x80000000);
            bwhed.Write(0);
            bwhed.Write(listPackSize);
            bwhed.Write(listsize);
            bwout.Write(filelist);
            foreach (string i in files)
            {
                //if (System.IO.Path.GetExtension(i) != ".csv")
                //    continue;
                byte[] buf;
                int offset = (int)output.Position;
                System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Open);
                buf = new byte[fs.Length];
                fs.Read(buf, 0, buf.Length);
                fs.Close();
                int packSize = buf.Length;
                int size = buf.Length;
                    Pack(buf, out buf);
                    packSize = (int)(Convert.ToUInt32(buf.Length) | 0x80000000);
                
                bwhed.Write(offset);
                bwhed.Write(packSize);
                bwhed.Write(size);
                bwout.Write(buf);
            }
            output.Flush();
            output.Close();
            header.Flush();
            header.Close();

        }

        public void Close()
        {
            brdata.BaseStream.Flush();
            brhed.BaseStream.Flush();
            brdata.BaseStream.Close();
            brhed.BaseStream.Close();
        }
    }
}
