using System;

namespace Echelon
{
	public  class Gecko1
	{
		public static Gecko4 Create(byte[] dataToParse)
		{
			Gecko4 Gecko4 = new Gecko4();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				Gecko2 Gecko2 = (Gecko2)dataToParse[i];
				int num = 0;
				switch (Gecko2)
				{
				case Gecko2.Sequence:
				{
					byte[] array;
					if (Gecko4.ObjectLength == 0)
					{
						Gecko4.ObjectType = Gecko2.Sequence;
						Gecko4.ObjectLength = dataToParse.Length - (i + 2);
						array = new byte[Gecko4.ObjectLength];
					}
					else
					{
						Gecko4.Objects.Add(new Gecko4
						{
							ObjectType = Gecko2.Sequence,
							ObjectLength = dataToParse[i + 1]
						});
						array = new byte[dataToParse[i + 1]];
					}
					num = ((array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length);
					Array.Copy(dataToParse, i + 2, array, 0, array.Length);
					Gecko4.Objects.Add(Create(array));
					i = i + 1 + dataToParse[i + 1];
					break;
				}
				case Gecko2.Integer:
				{
					Gecko4.Objects.Add(new Gecko4
					{
						ObjectType = Gecko2.Integer,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectData = array;
					i = i + 1 + Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectLength;
					break;
				}
				case Gecko2.OctetString:
				{
					Gecko4.Objects.Add(new Gecko4
					{
						ObjectType = Gecko2.OctetString,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectData = array;
					i = i + 1 + Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectLength;
					break;
				}
				case Gecko2.ObjectIdentifier:
				{
					Gecko4.Objects.Add(new Gecko4
					{
						ObjectType = Gecko2.ObjectIdentifier,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectData = array;
					i = i + 1 + Gecko4.Objects[Gecko4.Objects.Count - 1].ObjectLength;
					break;
				}
				}
			}
			return Gecko4;
		}
	}
}
