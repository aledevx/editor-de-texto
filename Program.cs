
namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Abrir arquivo");
            Console.WriteLine("2 - Criar novo arquivo");
            Console.WriteLine("3 - Buscar por nome");
            Console.WriteLine("4 - Buscar por formato");
            Console.WriteLine("5 - Criar pasta");
            Console.WriteLine("0 - Sair");

            short options = short.Parse(GetRawInput());

            switch (options)
            {
                case 0: Environment.Exit(0); break;
                case 1: Abrir(); break;
                case 2: Editar(); break;
                case 3: BuscarPorNome(); break;
                case 4: BuscarPorFormato(); break;
                case 5: CriarPasta(); break;
                default: Menu(); break;
            }
        }
        static void Abrir()
        {
            Console.Clear();
            Console.WriteLine("Digite o caminho do arquivo:");
            string path = GetRawInput();

            using (var file = new StreamReader(path))
            {
                string text = file.ReadToEnd();
                Console.WriteLine(text);
            }
            Console.WriteLine("");
            Console.ReadLine();
            Menu();
        }
        static void Editar()
        {
            Console.Clear();
            Console.WriteLine("Digite seu texto abaixo (ESC para sair)");
            Console.WriteLine("------------------------------");
            string text = "";

            do
            {
                text += GetRawInput();
                text += Environment.NewLine;
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            Salvar(text);
        }
        static void Salvar(string text)
        {
            Console.Clear();
            Console.WriteLine("Qual o caminho para salvar o arquivo?");
            var path = GetRawInput();

            if (path == null)
            {
                Salvar(text);
                return;
            }

            using (var file = new StreamWriter(path))
            {
                file.Write(text);
            }

            Console.WriteLine($"Arquivo {path} salvo com sucesso!");
            Console.ReadLine();
            Menu();
        }
        static void BuscarPorNome()
        {
            Console.Clear();
            var path = "C:/dev/";
            var arquivos = Directory.GetFiles(path);
            Console.WriteLine("Digite o nome do arquivo que deseja modificar: ");
            string nomeArquivo = GetRawInput().ToLower();

            Console.WriteLine($"Arquivos na pasta que contém {nomeArquivo}:");

            foreach (var arquivo in arquivos)
            {
                if (nomeArquivo == "")
                {
                    Console.WriteLine(arquivo);
                }
                else if (arquivo.Contains(nomeArquivo))
                {
                    Console.WriteLine("-> " + arquivo.Substring(path.Length));
                }
            }
            Console.ReadLine();
            Menu();
        }
        static void BuscarPorFormato()
        {
            var path = "C:/dev/";
            Console.Clear();
            Console.WriteLine("Qual o formato do arquivo que deseja procurar?");
            var formato = GetRawInput();
            var arquivos = Directory.GetFiles(path);

            foreach (var arquivo in arquivos)
            {
                if (formato == "")
                {
                    Console.WriteLine("-> " + arquivo.Substring(path.Length));
                }
                else if (arquivo.EndsWith(formato))
                {
                    Console.WriteLine("-> " + arquivo.Substring(path.Length));
                }
            }
            Console.ReadLine();
            Menu();

        }
        static void CriarPasta()
        {
            var path = "C:/dev/";
            Console.Clear();
            Console.WriteLine("Digite o nome da pasta:");
            var nomePasta = GetRawInput();

            Directory.CreateDirectory(path + nomePasta);

            Console.WriteLine($"{nomePasta} criada com sucesso!");
            Console.ReadLine();
            Menu();
        }
        static string GetRawInput()
        {
            string? rawInput = Console.ReadLine()?.Trim();

            while (rawInput == null)
            {
                rawInput = Console.ReadLine();
            }

            return rawInput;
        }

    }
}
