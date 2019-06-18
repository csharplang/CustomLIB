using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomLIB.RandomLIB
{
    public static class Common
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static bool IsWeekend(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
        }
        static public int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }

        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
                age--;
            return age;
        }

        public static bool IsLeapYear(this DateTime value)
        {
            return (System.DateTime.DaysInMonth(value.Year, 2) == 29);
        }

        //Check to see if a date is between two dates. 'Nuff said.
        public static bool Between(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd)
        {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }

        public static string ToReadableTime(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        public static bool IsPalindrome(this string word)
        {
            int nLen, nHalfLen;
            bool bValid = true;

            nLen = word.Length - 1;
            nHalfLen = nLen / 2;
            for (int i = 0; i < nHalfLen; i++)
            {
                if (word.Substring(i, 1) != word.Substring(nLen - i, 1)) bValid = false;
            }

            return bValid;
        }
        public static Dictionary<string, string> Parameters(
              this Uri self)
        {
            return String.IsNullOrEmpty(self.Query)
              ? new Dictionary<string, string>()
              : self.Query.Substring(1).Split('&').ToDictionary(
                  p => p.Split('=')[0],
                  p => p.Split('=')[1]
              );
        }

        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            try
            {
                FileInfo oFileInfo = new FileInfo(filePath);
                bytes = oFileInfo.Length;
            }
            catch { }
            return bytes;
        }

        public static Boolean IsLeapDay(this DateTime date)
        {
            return (date.Month == 2 && date.Day == 29);
        }
        public static string Reverse(this string s)
        {
            char[] c = s.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }


        public static long FolderSize(this DirectoryInfo dir, bool bIncludeSub)
        {
            long totalFolderSize = 0;

            if (!dir.Exists) return 0;

            var files = from f in dir.GetFiles()
                        select f;
            foreach (var file in files) totalFolderSize += file.Length;

            if (bIncludeSub)
            {
                var subDirs = from d in dir.GetDirectories()
                              select d;
                foreach (var subDir in subDirs) totalFolderSize += FolderSize(subDir, true);
            }

            return totalFolderSize;
        }

        public static int Increment(this int i)
        {
            return ++i;
        }

        public static IEnumerable<T> RandomElements<T>(this IEnumerable<T> collection, int count = 0)
        {
            if (count > collection.Count() || count <= 0)
                count = collection.Count();

            List<int> usedIndices = new List<int>();
            Random random = new Random((int)DateTime.Now.Ticks);
            while (count > 0)
            {
                int index = random.Next(collection.Count());
                if (!usedIndices.Contains(index))
                {
                    yield return collection.ElementAt(index);
                    usedIndices.Add(index);
                    count--;
                }
            }
        }

        public static int KB(this int value)
        {
            return value * 1024;
        }
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
        public static bool IsWeekend2(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date;
            while (!nextDay.WorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }
        public static DateTime GetCurrentDate(this object source)
        {
            return DateTime.Now;
        }

        public static string LastChar(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length >= 1)
                {
                    return input.Substring(input.Length - 1, 1);
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return null;
            }
        }


        public static bool IsTrue(this bool value)
        {
            return value;
        }

        public static string ExcelColumnName(this int index)
        {
            var chars = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            index -= 1; //adjust so it matches 0-indexed array rather than 1-indexed column

            string columnName;

            var quotient = index / 26;
            if (quotient > 0)
                columnName = ExcelColumnName(quotient) + chars[index % 26];
            else
                columnName = chars[index % 26].ToString();

            return columnName;
        }

        public static string ExcelColumnName2(this int index)
        {
            var chars = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            index -= 1; //adjust so it matches 0-indexed array rather than 1-indexed column

            string columnName;

            var quotient = index / 26;
            if (quotient > 0)
                columnName = ExcelColumnName2(quotient) + chars[index % 26];
            else
                columnName = chars[index % 26].ToString();

            return columnName;
        }


        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }
        public static bool IsVowel(this char ch)
        {
            return "aeiouyáéíóúýa̋e̋i̋őűàèìòùỳầềồḕṑǜừằȁȅȉȍȕăĕĭŏŭy̆ắằẳẵặḝȃȇȋȏȗǎěǐǒǔy̌a̧ȩə̧ɛ̧i̧ɨ̧o̧u̧âêîôûŷḙṷẩểổấếốẫễỗậệộäëïöüÿṳḯǘǚṏǟȫǖṻȧėıȯẏǡạẹịọụỵậȩ̇ǡȱảẻỉỏủỷơướứờừởửỡữợựāǣēīōūȳḗṓȭǭąęįǫųy̨åi̊ůḁǻą̊ãẽĩõũỹаэыуояеёюийⱥɇɨøɵꝋʉᵿɏөӫұɨαεηιοωυάέήίόώύὰὲὴὶὸὼὺἀἐἠἰὀὠὐἁἑἡἱὁὡὑᾶῆῖῶῦἆἦἶὦὖἇἧἷὧὗᾳῃῳᾷῇῷᾴῄῴᾲῂῲᾀᾐᾠᾁᾑᾡᾆᾖᾦᾇᾗᾧϊϋΐΰῒῢῗῧἅἕἥἵὅὥὕἄἔἤἴὄὤὔἂἒἢἲὂὢὒἃἓἣἳὃὣὓᾅᾕᾥᾄᾔᾤᾂᾒᾢᾃᾓᾣæɯɪʏʊøɘɤəɛœɜɞʌɔɐɶɑɒιυ"
                     .Contains("" + ch);
        }

        public static double GetPercentage(this double value, int percentage)
        {
            var percentAsDouble = (double)percentage / 100;
            return value * percentAsDouble;
        }

        public static DateTime CurrentLocalTimeForTimeZone(this TimeZoneInfo tzi)
        {
            return System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, tzi.Id);
        }
        public static bool Like(this string value, string search)
        {
            return value.Contains(search) || value.StartsWith(search) || value.EndsWith(search);
        }

        public static string ReverseWords(this string sentence)
        {
            var words = sentence.Split(' ');
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        

    }
}