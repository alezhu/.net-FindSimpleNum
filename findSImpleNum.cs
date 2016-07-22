using System;
using System.IO;
using AZLib;
using System.Collections;

namespace FindSimpleNum {
	class App  {
		[STAThread]
		static public void Main() {
			string CurDir = GetCurDir();
			string SNFile = Path.Combine(CurDir,"simplenums.txt");
			ArrayList arNums = new ArrayList();

			if (File.Exists(SNFile)){
				string[] sNums = StringUtils.LoadStringFromFile(SNFile);
				arNums.Capacity = sNums.Length;
				foreach (string sNum in sNums){
					UInt64 num = UInt64.Parse(sNum);
					arNums.Add(num);
				}
			} else {
				arNums.Add((UInt64)1);
				arNums.Add((UInt64)2);
				arNums.Add((UInt64)3);
				using(StreamWriter writer = new StreamWriter(SNFile) ){
					writer.WriteLine("1\r\n2\r\n3");
					writer.Close();
				}
			}

			foreach (UInt64 num in arNums){
				Console.WriteLine(num);
			}

			Console.WriteLine("Find new num");
			
			UInt64 NextNum = (UInt64)arNums[arNums.Count -1];
			NextNum++;
			while (true){
				int iIndex = 1;
//				Console.WriteLine("NextNum {0}",NextNum);
				while (iIndex < arNums.Count){
					UInt64 CurNum = (UInt64)arNums[iIndex];
					iIndex++;
//					Console.WriteLine("{0} / {1} = {2}",NextNum,CurNum,NextNum % CurNum);
					if ((NextNum % CurNum) == 0){
//						Console.WriteLine("{0} / {1} = {2}",NextNum,CurNum,NextNum % CurNum);
						NextNum++;
						iIndex = 1;
					}
				}
				// если сюда пришли значит и на что не поделилось
				arNums.Add(NextNum);
				Console.WriteLine("{0}",NextNum);
				using(StreamWriter writer = new StreamWriter(SNFile,true) ){
					writer.WriteLine(NextNum);
					writer.Close();
				}
				NextNum++;
			}

		}
		static public string GetCurDir() {
			string _CurDir = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
			if (_CurDir == String.Empty)
			{
				_CurDir = Environment.CurrentDirectory;
			}
			return _CurDir;
		}
	};
	


}