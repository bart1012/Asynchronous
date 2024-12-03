namespace AsyncSprint
{
    internal class AsyncFileManager
    {
        public static async Task<string> ReadFile(string path)
        {
            string[] fileContent = await File.ReadAllLinesAsync(path);
            return string.Join("", fileContent);
        }

        public static async Task WriteToFile(string path, string textToWrite)
        {
            await File.WriteAllLinesAsync(path, textToWrite.Split(" "));
        }
    }
}
