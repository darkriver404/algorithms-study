using System;
using System.Text;

/* 1.2.1
 * 固定大小的日志缓冲区
 * 期待：环形队列
 * 状态：完成
 */
namespace fixed_log_buffer
{
    class Program
    {
        public enum LogType
        {
            Error = 0,
            Assert = 1,
            Warning = 2,
            Log = 3,
            Exception = 4
        }

        public struct LogData
        {
            public LogType logType { get; set; }
            public string tag { get; set; }
            public string message { get; set; }

            public LogData(LogType type, string t, string msg)
            {
                logType = type;
                tag = t;
                message = msg;
            }

            public override string ToString()
            {
                return string.Format("{0}\n", message);
            }
        }

        public class FixedLogBuffer
        {
            private LogData[] array;
            private int headIndex;
            private int tailIndex;

            public FixedLogBuffer(int size)
            {
                array = new LogData[size];
                headIndex = tailIndex = -1;
            }

            public void AddLog(LogData log)
            {
                if (headIndex == -1)
                {
                    headIndex = tailIndex = 0;
                    array[headIndex] = log;
                    return;
                }

                if ((tailIndex + 1) % array.Length == headIndex)
                {
                    headIndex = (headIndex + 1) % array.Length;
                }
                tailIndex = (tailIndex + 1) % array.Length;
                array[tailIndex] = log;
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                if (headIndex != -1)
                {
                    for (int i = headIndex; i != tailIndex; i = (i + 1) % array.Length)
                    {
                        builder.Append(array[i]);
                    }
                }
                builder.Append(array[tailIndex]);
                return builder.ToString();
            }
        }

        static void Main(string[] args)
        {
            FixedLogBuffer buffer = new FixedLogBuffer(5);
            for (int i = 0; i < 1000; i++)
            {
                buffer.AddLog(new LogData(LogType.Log, "tag", i.ToString()));
                Console.WriteLine(string.Format("--------{0}----------",i));
                Console.Write(buffer.ToString());
            }
            Console.ReadKey();
        }
    }
}
