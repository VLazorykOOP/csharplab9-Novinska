using System;
using System.Collections;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Надрукувати вміст текстового файлу, виписуючи літери кожного рядка у зворотному порядку");
            Console.WriteLine("2. Обробка файлу: друк слів на голосну та приголосну букву");
            Console.WriteLine("3. Клас ArrayList з використанням інтерфейсів");
            Console.WriteLine("4. Музикальний диск та пісня");
            Console.WriteLine("0. Вихід");

            int choice = int.Parse(Console.ReadLine() ?? "5");

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Введено некоректний варіант. Будь ласка, виберіть знову.");
                    break;
            }
        }
    }

    static void Task1()
    {
        string filePath = "C:\\Users\\pc\\Desktop\\csharplab9-Novinska\\Lab9_10CharpT\\t.txt";

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                ArrayList charList = new ArrayList();

                foreach (char ch in line)
                {
                    charList.Add(ch);
                }

                for (int i = charList.Count - 1; i >= 0; i--)
                {
                    Console.Write(charList[i]);
                }

                Console.WriteLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка при читанні файлу: " + e.Message);
        }
    }

    static void Task2()
    {
        string filePath = "C:\\Users\\pc\\Desktop\\csharplab9-Novinska\\Lab9_10CharpT\\t2.txt";

        Queue vowelQueue = new Queue();
        Queue consonantQueue = new Queue();

        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] words = line.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (Array.Exists(vowels, v => v == word[0]))
                    {
                        vowelQueue.Enqueue(word);
                    }
                    else
                    {
                        consonantQueue.Enqueue(word);
                    }
                }
            }

            Console.WriteLine("Слова, що починаються на голосну букву:");
            while (vowelQueue.Count > 0)
            {
                Console.Write(vowelQueue.Dequeue() + " ");
            }

            Console.WriteLine();

            Console.WriteLine("Слова, що починаються на приголосну букву:");
            while (consonantQueue.Count > 0)
            {
                Console.Write(consonantQueue.Dequeue() + " ");
            }

            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка при читанні файлу: " + e.Message);
        }
    }

    static void Task3()
    {
        string filePath = "C:\\Users\\pc\\Desktop\\csharplab9-Novinska\\Lab9_10CharpT\\t2.txt";

        ArrayList vowelWords = new ArrayList();
        ArrayList consonantWords = new ArrayList();

        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] words = line.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string wordText in words)
                {
                    Word word = new Word(wordText);

                    if (Array.Exists(vowels, v => v == word.Text[0]))
                    {
                        vowelWords.Add(word);
                    }
                    else
                    {
                        consonantWords.Add(word);
                    }
                }
            }

            Console.WriteLine("Слова, що починаються на голосну букву:");
            foreach (Word word in vowelWords)
            {
                Console.Write(word.Text + " ");
            }

            Console.WriteLine();

            Console.WriteLine("Слова, що починаються на приголосну букву:");
            foreach (Word word in consonantWords)
            {
                Console.Write(word.Text + " ");
            }

            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка при читанні файлу: " + e.Message);
        }
    }

    static void Task4()
    {
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddDisc("Best of 90s");
        catalog.AddSong("Best of 90s", "Skorpions", "Still loving you");
        catalog.AddSong("Best of 90s", "The flatwood Mac", "Chain");
        catalog.AddSong("Best of 90s", "Pink floyd", "Comfortably numb");

        Console.WriteLine("\n");

        catalog.AddDisc("Greatest Hits");
        catalog.AddSong("Greatest Hits", "Queen", "Bohemian Rhapsody");
        catalog.AddSong("Greatest Hits", "Queen", "Show must go on");


        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.RemoveSong("Best of 90s", "Pink floyd", "Comfortably numb");

        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.SearchByArtist("Queen");

        Console.ReadKey();
    }

    class Word : ICloneable
    {
        public string Text { get; set; }

        public Word(string text)
        {
            Text = text;
        }

        public object Clone()
        {
            return new Word(this.Text);
        }
    }

    class WordComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Word word1 = x as Word;
            Word word2 = y as Word;
            return string.Compare(word1.Text, word2.Text);
        }
    }

    class MusicCatalog
    {
        private Hashtable discs = new Hashtable();

        public void AddDisc(string discTitle)
        {
            if (!discs.ContainsKey(discTitle))
            {
                discs.Add(discTitle, new Hashtable());
                Console.WriteLine($"Диск {discTitle} додано.");
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} вже існує.");
            }
        }

        public void RemoveDisc(string discTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                discs.Remove(discTitle);
                Console.WriteLine($"Диск {discTitle} видалено.");
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }

        public void AddSong(string discTitle, string artist, string songTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                Hashtable disc = (Hashtable)discs[discTitle];
                if (!disc.ContainsKey(artist))
                {
                    disc.Add(artist, new ArrayList());
                }
                ArrayList songs = (ArrayList)disc[artist];
                if (!songs.Contains(songTitle))
                {
                    songs.Add(songTitle);
                    Console.WriteLine($"Пісню {songTitle} виконавця {artist} додано до диску {discTitle}.");
                }
                else
                {
                    Console.WriteLine($"Пісня {songTitle} виконавця {artist} вже існує на диску {discTitle}.");
                }
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }

        public void RemoveSong(string discTitle, string artist, string songTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                Hashtable disc = (Hashtable)discs[discTitle];
                if (disc.ContainsKey(artist))
                {
                    ArrayList songs = (ArrayList)disc[artist];
                    if (songs.Contains(songTitle))
                    {
                        songs.Remove(songTitle);
                        Console.WriteLine($"Пісню {songTitle} виконавця {artist} видалено з диску {discTitle}.");
                    }
                    else
                    {
                        Console.WriteLine($"Пісні {songTitle} виконавця {artist} не існує на диску {discTitle}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Виконавець {artist} не існує на диску {discTitle}.");
                }
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }

        public void DisplayCatalog()
        {
            Console.WriteLine("Каталог музичних дисків:");
            foreach (DictionaryEntry discEntry in discs)
            {
                Console.WriteLine($"Диск: {discEntry.Key}");
                Hashtable disc = (Hashtable)discEntry.Value;
                foreach (DictionaryEntry artistEntry in disc)
                {
                    Console.WriteLine($"  Виконавець: {artistEntry.Key}");
                    ArrayList songs = (ArrayList)artistEntry.Value;
                    foreach (string song in songs)
                    {
                        Console.WriteLine($"    Пісня: {song}");
                    }
                }
            }
        }

        public void SearchByArtist(string artist)
        {
            Console.WriteLine($"Пошук пісень виконавця {artist}:");
            bool found = false;
            foreach (DictionaryEntry discEntry in discs)
            {
                Hashtable disc = (Hashtable)discEntry.Value;
                if (disc.ContainsKey(artist))
                {
                    ArrayList songs = (ArrayList)disc[artist];
                    foreach (string song in songs)
                    {
                        Console.WriteLine($"  Диск: {discEntry.Key}, Пісня: {song}");
                        found = true;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"Пісень виконавця {artist} не знайдено.");
            }
        }
    }
}
