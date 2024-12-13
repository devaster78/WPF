﻿using GTranslate.Translators;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ResxTranslationLib
{
    public enum Status
    {
        OK,
        Working,
        Error,
        Message
    }

    public class Translator
    {
        //private const int PerEstimate = 1250;
        private const int PerEstimate = 100;


        public static readonly List<string> Codes = new List<string>
        {
			#region Code
			"af",		// Afrikaans
			"sq",		// Albanian
			"am",		// Amharic
			"ar",		// Arabic
			"hy",		// Armenian
			"az",		// Azerbaijani
			"eu",		// Basque
			"be",		// Belarusian
			"bn",		// Bengali
			"bs",		// Bosnian
			"bg",		// Bulgarian
			"ca",		// Catalan
			"ceb",		// Cebuano
			"ny",		// Chichewa - (n/a)
			"zh-CN",	// Chinese (Simplified)
			"zh-TW",	// Chinese (Traditional)
			"co",		// Corsican
			"hr",		// Croatian
			"cs",		// Czech
			"da",		// Danish
			"nl",		// Dutch
			"en",		// English
			"eo",		// Esperanto
			"et",		// Estonian
			"tl",		// Filipino = fil-PH
			"fi",		// Finnish
			"fr",		// French
			"fy",		// Frisian
			"gl",		// Galician
			"ka",		// Georgian
			"de",		// German
			"el",		// Greek
			"gu",		// Gujarati
			"ha",		// Hausa
			"ht",		// Haitian Creole... not supported by Windows?
			"haw",		// Hawaiian
			"he",		// Hebrew = he-IL
			"hi",		// Hindi
			"hmn",		// Hmong - (n/a)
			"hu",		// Hungarian
			"is",		// Icelandic
			"ig",		// Igbo
			"id",		// Indonesian
			"ga",		// Irish
			"it",		// Italian
			"ja",		// Japanese
			"jw",		// Javanese - (n/a)
			"kn",		// Kannada
			"kk",		// Kazakh
			"km",		// Khmer
			"rw",		// Kinyarwanda
			"ko",		// Korean
			"ku",		// Kurdish (Kurmanji)
			"ky",		// Kyrgyz
			"lo",		// Lao
			"la",		// Latin
			"lv",		// Latvian
			"lt",		// Lithuanian
			"lb",		// Luxembourgish
			"mk",		// Macedonian
			"mg",		// Malagasy
			"ms",		// Malay
			"ml",		// Malayalam
			"mt",		// Maltese
			"mi",		// Maori
			"mr",		// Marathi
			"mn",		// Mongolian
			"my",		// Myanmar (Burmese)
			"ne",		// Nepali
			"no",		// Norwegian
			"or",		// Odia (Oriya)
			"ps",		// Pashto
			"fa",		// Persian
			"pl",		// Polish
			"pt",		// Portuguese
			"pa",		// Punjabi
			"ro",		// Romanian
			"ru",		// Russian
			"sm",		// Samoan - (n/a)
			"sr",		// Serbian
			"gd",		// Scots Gaelic
			"st",		// Sesotho
			"sn",		// Shona
			"sd",		// Sindhi
			"si",		// Sinhala
			"sk",		// Slovak
			"sl",		// Slovenian
			"so",		// Somali
			"es",		// Spanish
			"su",		// Sundanese - (n/a)
			"sw",		// Swahili
			"sv",		// Swedish
			"tg",		// Tajik
			"ta",		// Tamil
			"tt",		// Tatar
			"te",		// Telugu
			"th",		// Thai
			"tr",		// Turkish
			"tk",		// Turkmen
			"uk",		// Ukrainian
			"ur",		// Urdu
			"ug",		// Uyghur
			"uz",		// Uzbek
			"vi",		// Vietnamese
			"cy",		// Welsh
			"xh",		// Xhosa
			"yi",		// Yiddis
			"yo",		// Yoruba
			"zu"		// Zulu
			#endregion Code
		};

        private static readonly Dictionary<string, string> FileCodeMap = new Dictionary<string, string>
        {
            { "tl", "fil-PH" },
            { "iw", "he-IL" }
        };

        private static readonly Dictionary<string, string> FileNameMap = new Dictionary<string, string>
        {
            { "ny", "Chichewa" },
            { "hmn", "Hmong" },
            { "ht", "Haitian Creole" },
            { "jw", "Javanese" },
            { "sm", "Samoan" },
            { "su", "Sudanese" }
        };

        private const string NL = "\n";
        private const string InflationPattern = @"([^\s](?:[^\w\d\s\p{L}]))|((?:[^\w\d\s\p{L}])[^\s])";

        private readonly Regex inflation = new Regex(InflationPattern);


        /// <summary>
        /// Hints root node
        /// </summary>
        public XElement Hints { get; private set; }


        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // Languages...

        /// <summary>
        /// Gets the Windows-specific culture name, e.g. en -> en-US, for naming 
        /// satellite assemblies
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetCultureName(string code)
        {
            return FileCodeMap.ContainsKey(code)
                ? FileCodeMap[code]
                : CultureInfo.GetCultureInfo(code).TextInfo.CultureName;
        }


        /// <summary>
        /// Gets the display name of the language for UI controls
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetDisplayName(string code)
        {
            if (FileCodeMap.ContainsKey(code))
            {
                code = FileCodeMap[code];
            }

            return FileNameMap.ContainsKey(code)
                ? FileNameMap[code]
                : CultureInfo.GetCultureInfo(code).DisplayName;
        }


        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // Data Management...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strings"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static bool Estimate(string path, out int strings, out int seconds)
        {
            return Estimate(path, out strings, PerEstimate, out seconds);
        }


        public static bool Estimate(string path, out int strings, int delayInMs, out int seconds)
        {
            try
            {
                var root = XElement.Load(path);
                var data = ResxProvider.CollectStrings(root);

                strings = data.Count;
                seconds = data.Count * delayInMs / 1000;

                return true;
            }
            catch
            {
                strings = seconds = 0;
                return false;
            }
        }


        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // Inflation test...

        public bool Inflated(string original, string translation)
        {
            if (original == null || translation == null)
            {
                return false;
            }

            var oi = inflation.Matches(original).Count;
            var ti = inflation.Matches(translation).Count;
            return oi != ti;
        }


        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // One...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fromCode"></param>
        /// <param name="toCode"></param>
        /// <returns></returns>
        public async Task<string> Translate(
            string text, string fromCode, string toCode, CancellationTokenSource cancellation,
            StatusCallback logger = null)
        {
            if (text.Trim() == string.Empty)
            {
                return text;
            }

            using (var translator = new AggregateTranslator())
            {
                if (fromCode == "auto")
                {
                    var lang = await translator.DetectLanguageAsync(text);
                    fromCode = lang.ISO6391;
                }

                var result = await translator.TranslateAsync(text, toCode, fromCode);
                return result.Translation;
            }
        }


        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // Resx...

        /*
		 * 
		 * NOTE, tried to batch up strings, up to 1K of chars, with a delimiter,
		 * but the free Google Translator doesn't handle most languages correctly;
		 * e.g. given a batch with 10 strings, Google may only return 7 of them
		 * and it's unclear which 7 it will return?
		 * 
		 */

        public delegate void StatusCallback(string message, Color? color = null, bool increment = false);


        public void LoadHints(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    Hints = XElement.Load(path);
                    return;
                }
                catch
                {
                    // no-op
                }
            }

            Hints = new XElement("hints");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="fromCode"></param>
        /// <param name="toCode"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public async Task<bool> TranslateResx(
            List<XElement> data, string fromCode, string toCode,
            CancellationTokenSource cancellation,
            StatusCallback logger)
        {
            int index = 0;
            int count = 1;
            for (; index < data.Count && !cancellation.IsCancellationRequested; index++, count++)
            {
                var value = data[index].Element("value").Value;

                if (value.Length == 0)
                {
                    continue;
                }

                var editing = data[index].Element("comment")?.Value.ContainsICIC("!EDIT") == true;

                var name = editing
                    ? $"{data[index].Attribute("name").Value} (EDITED)"
                    : data[index].Attribute("name").Value;

                logger($"{count}/{data.Count}: {name}", increment: true);

                var hint = FindHint(data[index], logger);
                if (hint != null)
                {
                    data[index].Element("value").Value = hint;

                    // 2192 is right-arrow
                    logger($" \u2192 using hint override '{hint}'" + NL, Color.SteelBlue);

                    if (editing)
                    {
                        logger("Hint override will likely need updating", Color.Red);
                    }
                }
                else
                {
                    var builder = new StringBuilder();

                    // handle values with multiple lines (comboboxes)
                    var parts = value.Split('\n');
                    for (int i = 0; i < parts.Length && !cancellation.IsCancellationRequested; i++)
                    {
                        if (parts[i].Trim().Length == 0)
                        {
                            builder.Append(NL);
                        }
                        else
                        {
                            var result = await TranslateWithRetry(
                                parts[i], fromCode, toCode, cancellation, logger);

                            if (!string.IsNullOrEmpty(result) && !cancellation.IsCancellationRequested)
                            {
                                builder.Append(result);

                                if (i < parts.Length - 1)
                                    builder.Append(NL);
                            }
                        }
                    }

                    if (builder.Length > 0)
                    {
                        var result = builder.ToString();
                        data[index].Element("value").Value = result;

                        // 2192 is right-arrow
                        logger($" \u2192 '{value}' to '{result}'" + NL);

                        if (Inflated(value, result))
                        {
                            logger("*** possible inflation detected ***" + NL, Color.Maroon);
                        }
                    }
                    else
                    {
                        logger("unknown error" + NL, Color.Red);
                    }
                }
            }

            return index == data.Count;
        }


        private string FindHint(XElement data, StatusCallback logger)
        {
            var name = data.Attribute("name").Value;
            var hint = Hints.Elements()
                .FirstOrDefault(e => e.Attribute("name").Value == name);

            if (hint != null)
            {
                if (data.Element("value").Value.Trim() == hint.Element("source").Value.Trim())
                {
                    return hint.Element("preferred").Value.Trim();
                }

                logger($" \u2192 hint override is obsolete", Color.Maroon);
            }

            return null;
        }


        private async Task<string> TranslateWithRetry(
            string text, string fromCode, string toCode,
            CancellationTokenSource cancellation, StatusCallback logger)
        {
            using (var translator = new AggregateTranslator())
            {
                var result = await translator.TranslateAsync(text, toCode, fromCode);
                return result.Translation;
            }
        }


        /// <summary>
        /// Clear the !EDIT marker in the given data element
        /// </summary>
        /// <param name="data">A translation data element with a comment child</param>
        public static string ClearMarker(string comment)
        {
            return Regex.Replace(comment, @"\b!EDIT\b", string.Empty, RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// Remove the !EDIT markers from the source file after we're done applying
        /// the changes to all output files
        /// </summary>
        /// <param name="path"></param>
        public static int ClearMarkers(string path)
        {
            var root = XElement.Load(path);
            var comments = root.Elements("data").Elements("comment")
                .Where(e => e.Value.ContainsICIC("!EDIT"));

            if (comments.Any())
            {
                foreach (var comment in comments)
                {
                    comment.Value = ClearMarker(comment.Value);
                }

                root.Save(path);
            }

            return comments.Count();
        }
    }
}
