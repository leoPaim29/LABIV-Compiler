using System;
namespace Compilador
{
    public class Compiler
    {            
        public Compiler()
        {
        }

		static void Main(string[] args)
		{

            // Display the number of command line arguments:
            System.Console.WriteLine(Utility.gen("jr r22,r5,10\nadd r22,r5,10\njr r22,r5,10\nadd r22,r5,10"));            //System.Console.WriteLine(Utility.immediateToBin(65555,16));

		}
    }


    public class Utility
	{
        

		public Utility()
		{
            
		}

        public static string gen(string code){

            string[] newLine = new string[] { "\n" };
            string[] lines = code.Split(newLine,StringSplitOptions.None);
            string binaryCode = "";
            foreach (string line in lines){
				string[] tokens = line.Replace(" ", ",").Split(',');
				switch (tokens[0])
				{
					case "lw":
						binaryCode += $"{genericInstruction1("001001", tokens[1], tokens[2], tokens[3])}\n";
                        break;
					case "sw":
						binaryCode += $"{genericInstruction1("001010", tokens[1], tokens[2], tokens[3])}\n";
						break;
					case "add":
						binaryCode += $"{genericInstruction1("010001", tokens[1], tokens[2], tokens[3])}\n";
						break;
					case "sub":
						binaryCode += $"{genericInstruction1("010010", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "mul":
						binaryCode += $"{genericInstruction1("010100", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "div":
						binaryCode += $"{genericInstruction1("010101", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "and":
						binaryCode += $"{genericInstruction1("100001", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "or":
						binaryCode += $"{genericInstruction1("100010", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "cmp":
						binaryCode += $"{genericInstruction1("100100", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "not":
						binaryCode += $"{genericInstruction1("100010", tokens[1], tokens[2], tokens[3])}\n";
						break;

					case "jr":
                        binaryCode += $"{genericInstruction1("101001", tokens[1], tokens[2], tokens[3])}\n";
						break;

					default:
						binaryCode += $"Erro: Instrução desconhecida \"{tokens[0]}\"";
						break;

				}
            }

            return binaryCode;

        }

        //Returna a intrucao do grupo um em binario d
		public static string genericInstruction1(string opcode, string rd, string rb, string immediate)
		{
			return $"{opcode}{Utility.registerID(rd)}{Utility.registerID(rb)}{Utility.immediateToBin(Convert.ToInt32(immediate), 16)}";
		}

        //Retorna o id do registrador
        public static string registerID(string register){
            if(register.Length>3 || !((register.Substring(0,1) == "r") || (register.Substring(0, 1) == "R"))){
                return $"Erro: registrador \"{register}\" não existe";
            }else{
                return $"{immediateToBin(Convert.ToInt32(register.Substring(1, register.Length-1)),5)}";
            }
        }

        //Converte um valor inteiro imadiato em binario
        public static string immediateToBin(int value, int len)
		{
			if (value > 65535)
			{
                return $"Erro: Overflow em \"{value}\"";
			}
			else
			{
				return (len > 1 ? immediateToBin(value >> 1, len - 1) : null) + "01"[value & 1];
			}

		}

	}
}
