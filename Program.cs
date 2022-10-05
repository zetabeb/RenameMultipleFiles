using ZBB;

namespace RenameFiles
{
    class Program
    {
        //public static string exit;  
        public static string? choice;
        static ArchivoEntity ar =new ArchivoEntity();      
        static void Main(string[] args)
        {            
            string exit = "0";

            do{
                InitApp(ref exit);

                if(!SetPaths()) continue;

                if(!SetPhraseToRemove()) continue;

                SetFileFormat();

                List<string> archivos = FileManager.BuscarArchivos(ar.@dir, ar.format);

                if(archivos.Count > 0)
                {
                    Console.WriteLine("\n Archivos encontrados: ");
                    foreach(var n in archivos)
                    {
                        Console.WriteLine(n);
                    }

                    Console.WriteLine("\n ¿Desea continuar?");
                    Console.WriteLine("1 -> Si");
                    Console.WriteLine("2 -> No");
                    choice = Console.ReadLine();
                    if(!string.IsNullOrEmpty(choice) && choice.Replace(" ",string.Empty) != "1")
                    {
                        exit = "2";
                        continue;
                    }
                    StartProcess(archivos);
                }
                else Console.WriteLine("\n NO se encontraron archivos");                
                
                
                ExitProcess(ref exit);                

            }while(exit != "1");

            Console.WriteLine("Ha finalizado el proceso, presione cualquier tecla para salir");
            Console.ReadLine();
        }
        public static void InitApp(ref string exit)
        {
            ar.format ="";
            ar.phraseToRemove ="";
            ar.@dir ="";
            ar.@dir2 ="";
            choice = "";
            if(exit == "2") Console.WriteLine("No ha finalizado el proceso, la aplicacion se reinicia");
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("\n Aplicacion para quitar palabras no deseadas en nombres de multiples archivos dentro de una carpeta");
        }
        public static bool SetPaths()
        {            
            Console.WriteLine("\n ingresa la ruta de la carpeta donde estan los archivos");
            ar.@dir = Convert.ToString(Console.ReadLine());
            if (string.IsNullOrEmpty(ar.@dir) ||  !Directory.Exists(ar.@dir))
            {
                Console.WriteLine("No ha ingresado una ruta válida.");
                return false;
            }

            Console.WriteLine("\n ingresa la ruta de la carpeta donde van a quedar los nuevos archivos");
            ar.@dir2 = Convert.ToString(Console.ReadLine());
            if(!FileManager.VerficarCarpeta(ar.@dir2))
            {
                Console.WriteLine("No ha ingresado una ruta válida.");
                return false;
            }
            return true;
        }
        public static bool SetPhraseToRemove()
        {            
            Console.WriteLine("\n ingresa la frase o palabra que va a eliminar del nombre de los archivos");
            ar.phraseToRemove = Convert.ToString(Console.ReadLine());
            if (string.IsNullOrEmpty(ar.@phraseToRemove))
            {
                Console.WriteLine("No ha ingresado una frase o palabra. ");
                return false;
            }
            return true;
        }
        public static void SetFileFormat()
        {
            Console.WriteLine("\n ¿Desea tratar a archivos de algun formato en especifico?");
            Console.WriteLine("1 -> Si");
            Console.WriteLine("2 -> No");
            choice = Console.ReadLine();

            if(!string.IsNullOrEmpty(choice) && choice.Replace(" ",string.Empty) == "1")
            {
                Console.WriteLine("\n Ingrese el formato que desea tratar (ej: mp4)");
                string? format = Console.ReadLine();
                if(!string.IsNullOrEmpty(format))
                {
                    ar.format = format;
                }
                else Console.WriteLine("No ingresado un formato");
            }
            else Console.WriteLine("No se tratará un formato en especifico");

            if(String.Equals(ar.format,"")) Console.WriteLine("\n Se trataran todos los archivos de la ruta especificada");
            else Console.WriteLine("\n Se trataran todos los archivos de formato "+ar.format+" de la ruta especificada");            
        }
        public static void StartProcess(List<string> archivos)
        {
            Console.WriteLine("\n el proceso ha iniciado");
                
            List<string> newArchivos = StringEditor.RemovePhareFromList(archivos, ar.phraseToRemove);

            Console.WriteLine("\n Nuevos nombres");
            for(int i=0; i < archivos.Count(); i++)
            {
                Console.WriteLine("");
                Console.Write(archivos[i] + " -> ");
                Console.Write(newArchivos[i]);

                File.Copy(Path.Combine(ar.@dir,archivos[i]) , Path.Combine(ar.@dir2,newArchivos[i]));
            }
        }
        public static void ExitProcess(ref string exit)
        {
            Console.WriteLine("\n ¿Desea salir de la aplicacion? (escriba el numero correspondiente)");
            Console.WriteLine("1 -> Si");
            Console.WriteLine("2 -> No");
            choice = Console.ReadLine();

            if(!string.IsNullOrEmpty(choice))
            {
                if(choice.Replace(" ", string.Empty) == "1") exit = "1";
                else exit ="2";
            }
        }
    }
}