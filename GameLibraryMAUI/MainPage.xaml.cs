using LittleBitsGameLibrary;

namespace GameLibraryMAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            ReloadGameList();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            Game game = new Game(TitleEntry.Text, DevEntry.Text, DescEditor.Text, "", GenreEntry.Text);
            var serializer = new YamlDotNet.Serialization.Serializer();
            string yamlGame = serializer.Serialize(game);

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (FileName.Text.EndsWith(".yaml"))
                {
                    System.IO.File.WriteAllText($"{documentsFolder}\\LittleBitGameLibrary\\games\\{FileName.Text}", yamlGame);
                }
                else
                {
                    System.IO.File.WriteAllText($"{documentsFolder}\\LittleBitGameLibrary\\games\\{FileName.Text}.yaml", yamlGame);
                }
            }

            ReloadGameList();
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            TitleEntry.Text = "";
            DescEditor.Text = "";
            GamePicker.SelectedItem = null;
        }


        private void PickedGameChange(object sender, EventArgs e)
        {
            if(GamePicker.SelectedItem != null || GamePicker.SelectedItem.ToString() != String.Empty)
            {
                string yamlGame = "";
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    yamlGame = System.IO.File.ReadAllText($"{documentsFolder}\\LittleBitGameLibrary\\games\\{GamePicker.SelectedItem}");
                }
                var deserializer = new YamlDotNet.Serialization.Deserializer();
                Game game = deserializer.Deserialize<Game>(yamlGame);
                TitleEntry.Text = game.name;
                DevEntry.Text = game.developer;
                DescEditor.Text = game.description;
                GenreEntry.Text = game.genre;
                FileName.Text = GamePicker.SelectedItem.ToString();
            }
        }

        private void ReloadGameList()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                GamePicker.Items.Clear();
                if (Directory.Exists($"{documentsFolder}\\LittleBitGameLibrary\\games"))
                {
                    string[] files = System.IO.Directory.GetFiles($"{documentsFolder}\\LittleBitGameLibrary\\games");
                    foreach (string file in files)
                    {
                        if (file.EndsWith(".yaml"))
                        {
                            string[] splitname = file.Split('\\');
                            string name = splitname[splitname.Length - 1];
                            GamePicker.Items.Add(name);
                        }
                    }
                    
                    if(FileName.Text != String.Empty)
                    {
                        if (GamePicker.Items.Contains(FileName.Text))
                        {
                            GamePicker.SelectedIndex = GamePicker.Items.IndexOf(FileName.Text);
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory($"{documentsFolder}\\LittleBitGameLibrary\\games");
                }

            }
        }
    }
}
