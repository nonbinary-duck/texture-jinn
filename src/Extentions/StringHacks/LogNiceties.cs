namespace TextureJinn.Extentions.StringHacks
{
    public static class LogNiceties
    {
        private const string StandardSeperator = "====================";
        private const string LineEnding = "\n";

        /// <summary> Appens a standardised seperator </summary>
        public static string AppendSeperator(this string str) {
            return string.Concat(str, StandardSeperator);
        }

        /// <summary> Appens a line ending then a sepperator </summary>
        public static string AppendLineSeperator(this string str) {
            return string.Concat(str, LineEnding, StandardSeperator);
        }

        /// <summary> Appens a line ending, a sepperator then one more line ending to the end of that </summary>
        public static string AppendPaddedSep(this string str) {
            return string.Concat(str, LineEnding, StandardSeperator, LineEnding);
        }

        /// <summary> Appens two line endings, a sepperator then two line endings to the end of that </summary>
        public static string Append2PadSep(this string str) {
            return string.Concat(str, LineEnding, LineEnding, StandardSeperator, LineEnding, LineEnding);
        }

        /// <summary> Appens a sepperator then a single line ending to the end of that </summary>
        public static string AppendPstPadSep(this string str) {
            return string.Concat(str, StandardSeperator, LineEnding);
        }

        /// <summary> Appens a sepperator then two line endings to the end of that </summary>
        public static string AppendPst2PadSep(this string str) {
            return string.Concat(str, StandardSeperator, LineEnding, LineEnding);
        }
    }
}