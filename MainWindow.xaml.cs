using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Windows.Controls;
using File = System.IO.File;
using System.IO;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace 绝区零属性自动截图计算
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        public MainWindow()
        {
            InitializeComponent();
            _instance = this;
        }

        public static MainWindow Instance
        {
            get { return _instance; }
        }

        public void 打印到高级文本框(string text)
        {
            cmd输出.Document.Blocks.Add(new Paragraph(new Run(text)));
            cmd输出.ScrollToEnd();
        }

        public void 高级文本框清空内容()
        {
            cmd输出.Document.Blocks.Clear();
        }

        private void 一键截图事件(object sender, RoutedEventArgs e)
        {
            // 获取目录中的所有文件
            string[] files = Directory.GetFiles(@"py\img\");
            foreach (string file in files)
            {
                File.Delete(file);  // 删除文件
            }
            
            截图.游戏截图();

            IntPtr 此程序句柄 = 窗口.获取窗口句柄("绝区零属性权重计算器");
            if (跨类变量.截图结果)
            {
                图片验证成功:
                截图提示.Text = "截图成功";
                窗口.窗口激活(此程序句柄);
                Process.Start("./py/属性识别.bat");
            }
            else
            {
                截图失败:
                截图提示.Text = "截图失败,请确认设置正确后重新点击截图";
                窗口.窗口激活(此程序句柄);
            }
        }

        private void 最小化_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void 关闭_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void 拖动窗口(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void 加载事件(object sender, RoutedEventArgs e)
        {
            数据计算.加载主属性();
        }

        private void 关于事件(object sender, RoutedEventArgs e)
        {
            cmd输出.Document.Blocks.Clear();
            打印到高级文本框("绝区零属性权重计算器: V2.0");
            打印到高级文本框("伤害公式出自b站UP[睡觉宝]大佬,感谢");
            打印到高级文本框("作者: 豆馅");
        }

        private void 更新属性库事件(object sender, RoutedEventArgs e)
        {
            Process.Start("./py/同步角色与音擎库.bat");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("./py/config.txt"))
            {
                MessageBox.Show("程序首次运行需配置环境依赖, 点击确定安装。安装过程中请勿关闭窗口!\n\n如安装失败请在主界面点击右键选择依赖环境修复", "依赖环境未安装!");
                Process.Start("./py/安装依赖环境.bat");
                FileStream fs = File.Create("./py/config.txt");
            }
        }

        private void 视频教程事件(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.bilibili.com/video/BV1gKmGY5EGy/",
                UseShellExecute = true // 使用系统默认浏览器打开链接  
            });
        }

        private void 副属性加载事件(object sender, RoutedEventArgs e)
        {
            数据计算.加载副属性();
        }

        private void 依赖环境修复事件(object sender, RoutedEventArgs e)
        {
            Process.Start("./py/安装依赖环境.bat");
        }

        private void 打开截图目录事件(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", @"py\img\");
        }

        private void 打开存档目录事件(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", @"py\save\");
        }

        private void 高级功能选中(object sender, RoutedEventArgs e)
        {
            this.Height = 905;
        }

        private void 原属性输入框输入内容事件(object sender, TextCompositionEventArgs e)
        {
            // 检查每个字符是否为数字或小数点
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    e.Handled = true; // 阻止非数字和非小数点字符的输入
                    continue;
                }

                // 如果已经是小数点，并且输入的也是小数点，则阻止
                TextBox textBox = sender as TextBox;
                if (c == '.' && textBox.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void 原属性文本失去焦点事件(object sender, RoutedEventArgs e)
        {
            if (跨类变量.是否写好主属性)
            {
                数据计算.界面属性展示更新();
            }
        }

        private void 角色等级计算攻击基数事件(object sender, RoutedEventArgs e)
        {
            double 角色等级 = Convert.ToDouble(角色等级输入框.Text);
            double 攻击基数 = Math.Round(0.15541 * 角色等级 * 角色等级 + 3.1226 * 角色等级 + 46.951);
            变更攻击基数文本框.Text = 攻击基数.ToString();
            double 异常基数 = (角色等级 - 1)*1 / 59 + 1;
            异常基数文本框.Text = 异常基数.ToString();
        }

        private void 怪物等级计算怪物防御事件(object sender, RoutedEventArgs e)
        {
            double 怪物等级 = Convert.ToDouble(怪物等级输入框.Text);
            double 怪物防御 = Math.Round((0.15541 * 怪物等级 * 怪物等级 + 3.1226 * 怪物等级 + 46.951) / 50 * 60);
            变更怪物防御文本框.Text = 怪物防御.ToString();
        }

        private void 高级功能取消选中(object sender, RoutedEventArgs e)
        {
            this.Height = 715;
        }

        private void 属性变更计算并输出结果(object sender, RoutedEventArgs e)
        {
            数据计算.属性变更计算并输出结果();
        }

        private void 一键抓包事件(object sender, RoutedEventArgs e)
        {
            Process.Start("./py/米游社抓包.bat");
        }

        private void 依赖环境修复事件2(object sender, RoutedEventArgs e)
        {
            Process.Start("./py/海外安装依赖环境.bat");
        }
    }

    public static class 数据计算
    {
        static Dictionary<string, dynamic> 属性字典 = new Dictionary<string, dynamic>();
        static Dictionary<string, dynamic> 武器字典 = new Dictionary<string, dynamic>();
        static Dictionary<string, dynamic> 副属性字典 = new Dictionary<string, dynamic>();
        static Dictionary<string, dynamic> 原属性字典 = new Dictionary<string, dynamic>();
        static Dictionary<string, dynamic> 原属性字典2 = new Dictionary<string, dynamic>()
        {
            { "暴击率", 0 },{ "暴击伤害", 0 },{ "基础生命值", 0 },{ "生命值", 0 },{ "小生命值", 0 },{ "基础攻击力", 0 },{ "攻击力", 0 },{ "小攻击力", 0 },{ "基础防御力", 0 },{ "防御力", 0 },
            { "小防御力", 0 },{ "穿透值", 0 },{ "穿透率", 0 },{ "物理伤害加成", 0 },{ "火属性伤害加成", 0 },{ "冰属性伤害加成", 0 },{ "电属性伤害加成", 0 },{ "以太伤害加成", 0 },{ "异常精通", 0 },
            { "基础异常掌控", 0 },{ "异常掌控", 0 },{ "基础冲击力", 0 },{ "冲击力", 0 },{ "基础能量自动回复", 0 },{ "能量自动回复", 0 },{ "无", 0 }
        };
        static Dictionary<string, dynamic> 变更属性字典 = new Dictionary<string, dynamic>();
        static Dictionary<string, dynamic> 自定义属性字典 = new Dictionary<string, dynamic>();
        static Dictionary<int, string> 驱动盘名 = new Dictionary<int, string>();
        static Dictionary<int, Dictionary<string, dynamic>> 驱动盘主属性字典 = new Dictionary<int, Dictionary<string, dynamic>>();
        static Dictionary<int, Dictionary<string, dynamic>> 驱动盘副属性字典 = new Dictionary<int, Dictionary<string, dynamic>>();
        static string[] 驱动盘 = new string[7];
        static string 角色主属性, 副角色主属性, 角色特性, 副角色特性, 角色名, 副角色名, 武器名, 武器基础攻击力, 武器高级属性;
        static double 武器高级属性值, 直伤技能倍率;


        public static void 加载主属性()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;

            string 程序路径 = AppContext.BaseDirectory;
            // 创建一个文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的初始目录和过滤器（例如显示所有文件）
            openFileDialog.InitialDirectory = 程序路径 + @"py\save\";
            openFileDialog.Filter = "文本文件 (*.txt*)|*.txt*";

            // 显示对话框并处理结果
            if (openFileDialog.ShowDialog() == true)
            {
                string 文本路径 = openFileDialog.FileName;

                string 文本 = File.ReadAllText(文本路径);

                var 总属性字典 = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(文本);

                角色主属性 = 总属性字典["角色属性"]["角色属性"].ToString();
                角色特性 = 总属性字典["角色属性"]["角色特性"].ToString();
                角色名 = 总属性字典["角色属性"]["角色名"].ToString();

                武器字典 = 总属性字典["音擎属性"];
                武器名 = 武器字典["音擎名"].ToString();
                武器基础攻击力 = 武器字典["基础攻击力"].ToString();
                武器高级属性 = 取字典键(武器字典, 3).ToString();
                try
                {
                    武器高级属性值 = 武器字典[武器高级属性];
                }
                catch (Exception)
                {
                    武器名 = "无";
                    武器高级属性 = "无";
                    武器高级属性值 = 0;
                }


                属性字典.Clear();
                原属性字典.Clear();

                属性字典 = 字典合并取浮点数(原属性字典2, 总属性字典["角色属性"], 总属性字典["核心技属性"], 总属性字典["音擎属性"], 总属性字典["两件套属性"],
                    总属性字典["驱动盘主属性1"], 总属性字典["驱动盘主属性2"], 总属性字典["驱动盘主属性3"], 总属性字典["驱动盘主属性4"], 总属性字典["驱动盘主属性5"], 总属性字典["驱动盘主属性6"],
                    总属性字典["驱动盘随机属性1"], 总属性字典["驱动盘随机属性2"], 总属性字典["驱动盘随机属性3"], 总属性字典["驱动盘随机属性4"], 总属性字典["驱动盘随机属性5"], 总属性字典["驱动盘随机属性6"]);

                属性字典["易伤率"] = 0;
                属性字典["减防率"] = 0;
                属性字典["减抗率"] = 0;


                for (int i = 1; i <= 6; i++)
                {
                    驱动盘主属性字典[i] = new Dictionary<string, dynamic>();
                    驱动盘副属性字典[i] = new Dictionary<string, dynamic>();
                    驱动盘主属性字典[i] = 字典合并取浮点数(总属性字典["驱动盘主属性" + i]);
                    驱动盘副属性字典[i] = 字典合并取浮点数(总属性字典["驱动盘随机属性" + i]);
                    驱动盘名[i] = 总属性字典["驱动盘主属性" + i]["驱动盘名"];
                }

                Window.原属性基础攻击力输入框.Text = 属性字典["基础攻击力"].ToString("R3");
                Window.原属性攻击力输入框.Text = (属性字典["攻击力"] * 100).ToString("G3");
                Window.原属性小攻击力输入框.Text = 属性字典["小攻击力"].ToString("R3");
                Window.原属性属性增伤输入框.Text = (属性字典[角色主属性] * 100).ToString("G3");
                Window.原属性暴击率输入框.Text = (属性字典["暴击率"] * 100).ToString("G3");
                Window.原属性暴击伤害输入框.Text = (属性字典["暴击伤害"] * 100).ToString("G3");
                Window.原属性异常精通输入框.Text = 属性字典["异常精通"].ToString("R3");
                Window.原属性减防率输入框.Text = (属性字典["减防率"] * 100).ToString("G3");
                Window.原属性穿透率输入框.Text = (属性字典["穿透率"] * 100).ToString("G3");
                Window.原属性穿透值输入框.Text = 属性字典["穿透值"].ToString("R3");
                Window.原属性减抗率输入框.Text = (属性字典["减抗率"] * 100).ToString("G3");
                Window.原属性易伤率输入框.Text = (属性字典["易伤率"] * 100).ToString("G3");
                跨类变量.是否写好主属性 = true;
                保存重置属性字典("保存");
                界面属性展示更新();
            }
            else
            {
                Window.打印到高级文本框("未选择文件,请重新选择");
            }
        }

        public static void 加载副属性()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            string 程序路径 = AppContext.BaseDirectory;
            // 创建一个文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的初始目录和过滤器（例如显示所有文件）
            openFileDialog.InitialDirectory = 程序路径 + @"py\save\";
            openFileDialog.Filter = "文本文件 (*.txt*)|*.txt*";

            // 显示对话框并处理结果
            if (openFileDialog.ShowDialog() == true)
            {
                string 文本路径2 = openFileDialog.FileName;

                string 文本2 = File.ReadAllText(文本路径2);

                var 总属性副字典 = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(文本2);

                副角色主属性 = 总属性副字典["角色属性"]["角色属性"].ToString();
                副角色特性 = 总属性副字典["角色属性"]["角色特性"].ToString();
                副角色名 = 总属性副字典["角色属性"]["角色名"].ToString();
                副属性字典.Clear();

                副属性字典 = 字典合并取浮点数(原属性字典2, 总属性副字典["角色属性"], 总属性副字典["核心技属性"], 总属性副字典["音擎属性"], 总属性副字典["两件套属性"],
                    总属性副字典["驱动盘主属性1"], 总属性副字典["驱动盘主属性2"], 总属性副字典["驱动盘主属性3"], 总属性副字典["驱动盘主属性4"], 总属性副字典["驱动盘主属性5"], 总属性副字典["驱动盘主属性6"],
                    总属性副字典["驱动盘随机属性1"], 总属性副字典["驱动盘随机属性2"], 总属性副字典["驱动盘随机属性3"], 总属性副字典["驱动盘随机属性4"], 总属性副字典["驱动盘随机属性5"], 总属性副字典["驱动盘随机属性6"]);

                副属性字典["易伤率"] = 0;
                副属性字典["减防率"] = 0;
                副属性字典["减抗率"] = 0;

                double 副属性最终伤害 = 副属性伤害计算();
                double 最终伤害 = 伤害计算();

                if (角色名 != 副角色名)
                {
                    Window.高级文本框清空内容();
                    Window.打印到高级文本框("警告,此功能仅适用于同角色不同配装对比,不能用于跨角色对比强度,各角色攻击倍率,频率均不相同!");
                }
                else
                {
                    Window.高级文本框清空内容();
                }

                if (副角色特性 == "异常" && 角色特性 == "异常")
                {
                    string 异常类型;
                    if (角色主属性 == "物理伤害加成")
                    {
                        异常类型 = "强击";
                    }
                    else if (角色主属性 == "冰属性伤害加成")
                    {
                        异常类型 = "碎冰";
                    }
                    else if (角色主属性 == "电属性伤害加成")
                    {
                        异常类型 = "感电";
                    }
                    else if (角色主属性 == "火属性伤害加成")
                    {
                        异常类型 = "灼烧";
                    }
                    else if (角色主属性 == "以太伤害加成")
                    {
                        异常类型 = "侵蚀";
                    }
                    else
                    {
                        异常类型 = "";
                    }

                    Window.打印到高级文本框($"{角色名}主属性{异常类型}伤害为:[{最终伤害}],{副角色名}对比属性异常伤害为:[{副属性最终伤害}],对比属性最终伤害{计算两次伤害的差值(最终伤害, 副属性最终伤害)}!");
                }
                else if (副角色特性 != "异常" && 角色特性 != "异常")
                {
                    Window.打印到高级文本框($"{角色名}主属性{直伤技能倍率}%倍率下属性伤害为:[{最终伤害}],{副角色名}对比属性{直伤技能倍率}%倍率下属性伤害为:[{副属性最终伤害}],对比属性最终伤害{计算两次伤害的差值(最终伤害, 副属性最终伤害)}!");
                }
                else
                {
                    Window.打印到高级文本框("异常角色与直伤角色无法对比伤害,请重新选择");
                }
            }
            else
            {
                Window.打印到高级文本框("未选择文件,请重新选择");
            }
        }

        static Dictionary<string, dynamic> 字典合并取浮点数(params Dictionary<string, object>[] dictionaries)
        {
            // 初始化一个新的字典来存储合并后的结果
            var result = new Dictionary<string, dynamic>();

            // 遍历每个输入字典
            foreach (var dict in dictionaries)
            {
                // 遍历当前字典中的每个键值对
                foreach (var pair in dict)
                {
                    // 检查当前键值对的值是否为 double 或 int 类型
                    if (pair.Value is double value)
                    {
                        // 如果当前键已经存在于结果字典中
                        if (result.ContainsKey(pair.Key))
                        {
                            // 将当前键对应的值累加到结果字典中
                            result[pair.Key] += value;
                        }
                        else
                        {
                            // 如果当前键不存在于结果字典中，则添加键值对
                            result.Add(pair.Key, value);
                        }
                    }
                    else if (pair.Value is int intValue)
                    {
                        // 将 int 类型的值转换为 double 类型
                        value = (double)intValue;

                        // 如果当前键已经存在于结果字典中
                        if (result.ContainsKey(pair.Key))
                        {
                            // 将当前键对应的值累加到结果字典中
                            result[pair.Key] += value;
                        }
                        else
                        {
                            // 如果当前键不存在于结果字典中，则添加键值对
                            result.Add(pair.Key, value);
                        }
                    }
                    else if (pair.Value is Int64 int64Value)
                    {
                        // 将 int 类型的值转换为 double 类型
                        value = (double)int64Value;

                        // 如果当前键已经存在于结果字典中
                        if (result.ContainsKey(pair.Key))
                        {
                            // 将当前键对应的值累加到结果字典中
                            result[pair.Key] += value;
                        }
                        else
                        {
                            // 如果当前键不存在于结果字典中，则添加键值对
                            result.Add(pair.Key, value);
                        }
                    }
                    else
                    {
                        Console.WriteLine(pair.Value.GetType());
                        // 如果值不是 double 或 int 类型，则忽略这个键值对
                        continue;
                    }
                }
            }
            // 返回合并后的结果字典
            return result;
        }

        private static void 保存重置属性字典(string 需求)
        {
            if (需求 == "保存")
            {
                原属性字典.Clear();
                foreach (var kvp in 属性字典)
                {
                    原属性字典.Add(kvp.Key, kvp.Value);
                }
            }
            else
            {
                属性字典.Clear();
                foreach (var kvp in 原属性字典)
                {
                    属性字典.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public static void 界面属性展示更新()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            if (跨类变量.是否写好主属性)
            {
                属性字典["基础攻击力"] = Convert.ToDouble(Window.原属性基础攻击力输入框.Text);
                属性字典["攻击力"] = Convert.ToDouble(Window.原属性攻击力输入框.Text) / 100;
                属性字典["小攻击力"] = Convert.ToDouble(Window.原属性小攻击力输入框.Text);
                属性字典[角色主属性] = Convert.ToDouble(Window.原属性属性增伤输入框.Text) / 100;
                属性字典["暴击率"] = Convert.ToDouble(Window.原属性暴击率输入框.Text) / 100;
                属性字典["暴击伤害"] = Convert.ToDouble(Window.原属性暴击伤害输入框.Text) / 100;
                属性字典["异常精通"] = Convert.ToDouble(Window.原属性异常精通输入框.Text);
                属性字典["减防率"] = Convert.ToDouble(Window.原属性减防率输入框.Text) / 100;
                属性字典["穿透率"] = Convert.ToDouble(Window.原属性穿透率输入框.Text) / 100;
                属性字典["穿透值"] = Convert.ToDouble(Window.原属性穿透值输入框.Text);
                属性字典["减抗率"] = Convert.ToDouble(Window.原属性减抗率输入框.Text) / 100;
                属性字典["易伤率"] = Convert.ToDouble(Window.原属性易伤率输入框.Text) / 100;
                属性字典["攻击基数"] = Convert.ToDouble(Window.变更攻击基数文本框.Text);
                属性字典["怪物防御"] = Convert.ToDouble(Window.变更怪物防御文本框.Text);
            }
            保存重置属性字典("保存");

            Window.武器属性文本框.Text = $"装备武器: < {武器名} > 武器属性:";
            Window.武器基础攻击力文本框.Text = 武器基础攻击力;
            Window.武器高级属性名称文本框.Text = 武器高级属性;
            if (武器高级属性值 < 1)
            {
                Window.武器高级属性文本框.Text = $"{武器高级属性值 * 100}%";
            }
            else
            {
                Window.武器高级属性文本框.Text = 武器高级属性值.ToString();
            }

            Window.面板属性文本框.Text = $"当前角色: < {角色名} > 面板属性:";
            Window.生命值属性文本框.Text = (Math.Round(属性字典["基础生命值"] * (1 + 属性字典["生命值"]) + 属性字典["小生命值"], 1)).ToString();
            Window.攻击力属性文本框.Text = (Math.Round(属性字典["基础攻击力"] * (1 + 属性字典["攻击力"]) + 属性字典["小攻击力"], 1)).ToString();
            Window.防御力属性文本框.Text = (Math.Round(属性字典["基础防御力"] * (1 + 属性字典["防御力"]) + 属性字典["小防御力"], 1)).ToString();
            Window.冲击力属性文本框.Text = (Math.Round(属性字典["基础冲击力"] * (1 + 属性字典["冲击力"]), 1)).ToString();
            Window.暴击率属性文本框.Text = (Math.Round((属性字典["暴击率"] * 100), 3)).ToString() + "%";
            Window.暴击伤害属性文本框.Text = (Math.Round((属性字典["暴击伤害"] * 100), 3)).ToString() + "%";
            Window.异常精通属性文本框.Text = 属性字典["异常精通"].ToString();
            Window.穿透值属性文本框.Text = 属性字典["穿透值"].ToString();
            Window.穿透率属性文本框.Text = (Math.Round((属性字典["穿透率"] * 100), 3)).ToString() + "%";
            Window.伤害加成属性文本框.Text = (Math.Round((属性字典[角色主属性] * 100), 3)).ToString() + "%";
            Window.伤害加成名称文本框.Text = 角色主属性;
            Window.异常掌控属性文本框.Text = (属性字典["基础异常掌控"] * (1 + 属性字典["异常掌控"])).ToString();
            Window.能量自动回复属性文本框.Text = (Math.Round(属性字典["基础能量自动回复"] * (1 + 属性字典["能量自动回复"]), 3)).ToString();

            驱动盘收益计算(1);
            驱动盘收益计算(2);
            驱动盘收益计算(3);
            驱动盘收益计算(4);
            驱动盘收益计算(5);
            驱动盘收益计算(6);

            单词条收益分析();
            驱动盘主属性分析();

            double 最终伤害 = 伤害计算();
            Window.高级文本框清空内容();
            if (角色特性 == "异常")
            {
                if (角色主属性 == "物理伤害加成")
                {
                    Window.打印到高级文本框($"当前属性下,{角色名}强击[713%]倍率造成的伤害为:{最终伤害}");
                }
                else if (角色主属性 == "冰属性伤害加成")
                {
                    Window.打印到高级文本框($"当前属性下,{角色名}碎冰[500%]倍率造成的伤害为:{最终伤害}");
                }
                else if (角色主属性 == "电属性伤害加成")
                {
                    Window.打印到高级文本框($"当前属性下,{角色名}感电[125%]倍率造成的伤害为:{最终伤害}");
                }
                else if (角色主属性 == "火属性伤害加成")
                {
                    Window.打印到高级文本框($"当前属性下,{角色名}灼烧[50%]倍率造成的伤害为:{最终伤害}");
                }
                else if (角色主属性 == "以太伤害加成")
                {
                    Window.打印到高级文本框($"当前属性下,{角色名}侵蚀[62.5%]倍率造成的伤害为:{最终伤害}");
                }
            }
            else
            {
                Window.打印到高级文本框($"当前属性下,{角色名}[{直伤技能倍率}%]倍率的属性伤害为:{最终伤害}");
            }
        }

        private static Dictionary<string, dynamic> 返回理论满分直伤驱动盘随机属性字典(int 驱动盘号, Tuple<double, double, double, double> 词条强化分配)
        {
            Dictionary<string, dynamic> 满分盘 = new Dictionary<string, dynamic> { };
            满分盘.Clear();

            double 词条1强化 = 词条强化分配.Item1 + 1;
            double 词条2强化 = 词条强化分配.Item2 + 1;
            double 词条3强化 = 词条强化分配.Item3 + 1;
            double 词条4强化 = 词条强化分配.Item4 + 1;


            if (驱动盘号 == 1)
            {
                满分盘["暴击率"] = 0.024 * 词条1强化;
                满分盘["暴击伤害"] = 0.048 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["小攻击力"] = 19 * 词条4强化;
            }
            else if (驱动盘号 == 2)
            {
                满分盘["暴击率"] = 0.024 * 词条1强化;
                满分盘["暴击伤害"] = 0.048 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["穿透值"] = 9 * 词条4强化;
            }
            else if (驱动盘号 == 3)
            {
                满分盘["暴击率"] = 0.024 * 词条1强化;
                满分盘["暴击伤害"] = 0.048 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["小攻击力"] = 19 * 词条4强化;
            }
            else if (驱动盘号 == 4)
            {
                if (取字典键(驱动盘主属性字典[4], 1) == "暴击率")
                {
                    满分盘["穿透值"] = 9 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else if (取字典键(驱动盘主属性字典[4], 1) == "暴击伤害")
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["穿透值"] = 9 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else if (取字典键(驱动盘主属性字典[4], 1) == "攻击力")
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["穿透值"] = 9 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            else if (驱动盘号 == 5)
            {
                if (取字典键(驱动盘主属性字典[5], 1) == "攻击力")
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["穿透值"] = 9 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            else if (驱动盘号 == 6)
            {
                if (取字典键(驱动盘主属性字典[6], 1) == "攻击力")
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["穿透值"] = 9 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else
                {
                    满分盘["暴击率"] = 0.024 * 词条1强化;
                    满分盘["暴击伤害"] = 0.048 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            return 满分盘;
        }

        private static Dictionary<string, dynamic> 返回理论满分异常驱动盘随机属性字典(int 驱动盘号, Tuple<double, double, double, double> 词条强化分配)
        {
            Dictionary<string, dynamic> 满分盘 = new Dictionary<string, dynamic> { };
            满分盘.Clear();

            double 词条1强化 = 词条强化分配.Item1 + 1;
            double 词条2强化 = 词条强化分配.Item2 + 1;
            double 词条3强化 = 词条强化分配.Item3 + 1;
            double 词条4强化 = 词条强化分配.Item4 + 1;

            if (驱动盘号 == 1)
            {
                满分盘["穿透值"] = 9 * 词条1强化;
                满分盘["异常精通"] = 9 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["小攻击力"] = 19 * 词条4强化;
            }
            else if (驱动盘号 == 2)
            {
                满分盘["防御力"] = 0.048 * 词条1强化;
                满分盘["异常精通"] = 9 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["穿透值"] = 9 * 词条4强化;
            }
            else if (驱动盘号 == 3)
            {
                满分盘["穿透值"] = 9 * 词条1强化;
                满分盘["异常精通"] = 9 * 词条2强化;
                满分盘["攻击力"] = 0.03 * 词条3强化;
                满分盘["小攻击力"] = 19 * 词条4强化;
            }
            else if (驱动盘号 == 4)
            {
                if (取字典键(驱动盘主属性字典[4], 1) == "异常精通")
                {
                    满分盘["防御力"] = 0.048 * 词条1强化;
                    满分盘["穿透值"] = 9 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
                else if (取字典键(驱动盘主属性字典[4], 1) == "攻击力")
                {
                    满分盘["防御力"] = 0.048 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["小攻击力"] = 19 * 词条3强化;
                    满分盘["穿透值"] = 9 * 词条4强化;
                }
                else
                {
                    满分盘["穿透值"] = 9 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            else if (驱动盘号 == 5)
            {
                if (取字典键(驱动盘主属性字典[5], 1) == "攻击力")
                {
                    满分盘["防御力"] = 0.048 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["小攻击力"] = 19 * 词条3强化;
                    满分盘["穿透值"] = 9 * 词条4强化;
                }
                else
                {
                    满分盘["穿透值"] = 9 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            else if (驱动盘号 == 6)
            {
                if (取字典键(驱动盘主属性字典[6], 1) == "攻击力")
                {
                    满分盘["防御力"] = 0.048 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["小攻击力"] = 19 * 词条3强化;
                    满分盘["穿透值"] = 9 * 词条4强化;
                }
                else
                {
                    满分盘["穿透值"] = 9 * 词条1强化;
                    满分盘["异常精通"] = 9 * 词条2强化;
                    满分盘["攻击力"] = 0.03 * 词条3强化;
                    满分盘["小攻击力"] = 19 * 词条4强化;
                }
            }
            return 满分盘;
        }

        private static double 穷举理论满分驱动盘返回最大伤害(int 驱动盘号)
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 最大伤害 = 0; // 初始化最大伤害值为 0  
            Tuple<double, double, double, double> 词条强化分配 = null; // 存储加点方案  
            Dictionary<string, dynamic> 驱动盘属性 = new Dictionary<string, dynamic>();

            // 穷举所有加点方案，总共有 5 点属性点可分配  
            // 每个属性的加点范围从 0 点到 5 点  
            for (double 词条1 = 0; 词条1 <= 5; 词条1++)      // 词条1  
            {
                for (double 词条2 = 0; 词条2 <= 5; 词条2++)   // 词条2  
                {
                    for (double 词条3 = 0; 词条3 <= 5; 词条3++) // 词条3  
                    {
                        for (double 词条4 = 0; 词条4 <= 5; 词条4++)   // 暴击  
                        {
                            // 计算当前方案的总点数，减去每个属性最初的 1 点  
                            double 总点数 = 词条1 + 词条2 + 词条3 + 词条4;

                            // 确保总点数不超过 5 点  
                            if (总点数 == 5)
                            {
                                词条强化分配 = Tuple.Create(词条1, 词条2, 词条3, 词条4);

                                if (角色特性 == "异常")
                                {
                                    驱动盘属性 = 返回理论满分异常驱动盘随机属性字典(驱动盘号, 词条强化分配);
                                }
                                else
                                {
                                    驱动盘属性 = 返回理论满分直伤驱动盘随机属性字典(驱动盘号, 词条强化分配);
                                }

                                if (驱动盘号 == 1)
                                {
                                    属性变更(驱动盘副属性字典[1], "减");
                                }
                                else if (驱动盘号 == 2)
                                {
                                    属性变更(驱动盘副属性字典[2], "减");
                                }
                                else if (驱动盘号 == 3)
                                {
                                    属性变更(驱动盘副属性字典[3], "减");
                                }
                                else if (驱动盘号 == 4)
                                {
                                    属性变更(驱动盘副属性字典[4], "减");
                                }
                                else if (驱动盘号 == 5)
                                {
                                    属性变更(驱动盘副属性字典[5], "减");
                                }
                                else if (驱动盘号 == 6)
                                {
                                    属性变更(驱动盘副属性字典[6], "减");
                                }

                                属性变更(驱动盘属性, "加");
                                double 当前伤害 = 伤害计算();

                                if (当前伤害 > 最大伤害)
                                {
                                    最大伤害 = 当前伤害;
                                    string 驱动盘属性描述 = string.Join("", 驱动盘属性.Select(kv =>
                                        $"{kv.Key}:{(kv.Value < 1 ? $"{Math.Round(kv.Value * 100, 3)}%" : kv.Value.ToString())}\n"));
                                    if (驱动盘号 == 1)
                                    {
                                        Window.驱动盘1文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                    else if (驱动盘号 == 2)
                                    {
                                        Window.驱动盘2文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                    else if (驱动盘号 == 3)
                                    {
                                        Window.驱动盘3文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                    else if (驱动盘号 == 4)
                                    {
                                        Window.驱动盘4文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                    else if (驱动盘号 == 5)
                                    {
                                        Window.驱动盘5文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                    else if (驱动盘号 == 6)
                                    {
                                        Window.驱动盘6文本框.ToolTip = $"满分驱动盘属性:\n{驱动盘属性描述}驱动盘评分:100%";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // 返回找到的最佳加点方案  
            return 最大伤害;
        }

        private static void 驱动盘主属性分析()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 最终伤害 = 伤害计算();
            Window.驱动盘主属性推荐文本框.Text = "";

            自定义属性字典["暴击率"] = 0.24;
            属性变更(驱动盘主属性字典[4], "减");
            属性变更(自定义属性字典, "加");
            double 变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘4更换[暴击率 + 24%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典["暴击伤害"] = 0.48;
            属性变更(驱动盘主属性字典[4], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘4更换[暴击伤害 + 48%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典["攻击力"] = 0.3;
            属性变更(驱动盘主属性字典[4], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘4更换[攻击力 + 30%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典["异常精通"] = 92;
            属性变更(驱动盘主属性字典[4], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘4更换[异常精通 + 92点],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典[角色主属性] = 0.3;
            属性变更(驱动盘主属性字典[5], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘5更换[{角色主属性} + 30%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典["穿透率"] = 0.24;
            属性变更(驱动盘主属性字典[5], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘5更换[穿透率 + 24%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }

            自定义属性字典["攻击力"] = 0.3;
            属性变更(驱动盘主属性字典[5], "减");
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            自定义属性字典.Clear();
            if (变更伤害 > 最终伤害)
            {
                Window.驱动盘主属性推荐文本框.Text += $"驱动盘5更换[攻击力 + 30%],最终伤害将会{计算两次伤害的差值(最终伤害, 变更伤害)}\n";
            }
        }

        private static void 单词条收益分析()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 最终伤害 = 伤害计算();
            double 变更伤害;

            if (角色特性 == "异常")
            {
                Window.暴击率提升率文本框.Text = $"{角色名}异常角色不计算暴击率词条收益";
                Window.暴击伤害提升率文本框.Text = $"{角色名}为异常角色不计算暴击伤害词条收益";

                自定义属性字典["异常精通"] = 9;
                属性变更(自定义属性字典, "加");
                变更伤害 = 伤害计算();
                Window.精通值提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
                自定义属性字典.Clear();
            }
            else
            {
                Window.精通值提升率文本框.Text = $"{角色名}为直伤角色不计算异常精通词条收益";

                自定义属性字典["暴击率"] = 0.024;
                属性变更(自定义属性字典, "加");
                变更伤害 = 伤害计算();
                Window.暴击率提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
                自定义属性字典.Clear();

                自定义属性字典["暴击伤害"] = 0.048;
                属性变更(自定义属性字典, "加");
                变更伤害 = 伤害计算();
                Window.暴击伤害提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
                自定义属性字典.Clear();
            }

            自定义属性字典["攻击力"] = 0.03;
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            Window.攻击力提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
            自定义属性字典.Clear();

            自定义属性字典["小攻击力"] = 19;
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            Window.小攻击提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
            自定义属性字典.Clear();

            自定义属性字典["穿透值"] = 9;
            属性变更(自定义属性字典, "加");
            变更伤害 = 伤害计算();
            Window.穿透值提升率文本框.Text = 计算两次伤害的差值(最终伤害, 变更伤害);
            自定义属性字典.Clear();
        }

        private static void 属性变更(Dictionary<string, dynamic> 变更字典, string 需求)
        {
            foreach (var key in 变更字典.Keys)
            {
                if (属性字典.ContainsKey(key))
                {
                    if (需求 == "减")
                    {
                        属性字典[key] -= 变更字典[key];
                    }
                    else
                    {
                        属性字典[key] += 变更字典[key];
                    }
                }
            }
        }

        public static void 属性变更计算并输出结果()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 最终伤害 = 伤害计算();

            变更属性字典["基础攻击力"] = Convert.ToDouble(Window.变更基础攻击力文本框.Text);
            变更属性字典["攻击力"] = Convert.ToDouble(Window.变更攻击力文本框.Text) / 100;
            变更属性字典["小攻击力"] = Convert.ToDouble(Window.变更小攻击力文本框.Text);
            变更属性字典["暴击率"] = Convert.ToDouble(Window.变更暴击率文本框.Text) / 100;
            变更属性字典["暴击伤害"] = Convert.ToDouble(Window.变更暴击伤害文本框.Text) / 100;
            变更属性字典[角色主属性] = Convert.ToDouble(Window.变更伤害加成文本框.Text) / 100;
            变更属性字典["减抗率"] = Convert.ToDouble(Window.变更减抗率文本框.Text) / 100;
            变更属性字典["易伤率"] = Convert.ToDouble(Window.变更易伤率文本框.Text) / 100;
            变更属性字典["减防率"] = Convert.ToDouble(Window.变更减防率文本框.Text) / 100;
            变更属性字典["穿透率"] = Convert.ToDouble(Window.变更穿透率文本框.Text) / 100;
            变更属性字典["穿透值"] = Convert.ToDouble(Window.变更穿透值文本框.Text);
            变更属性字典["异常精通"] = Convert.ToDouble(Window.变更异常精通文本框.Text);

            属性变更(变更属性字典, "加");
            double 变更后最终伤害 = 伤害计算();
            string 结果 = 计算两次伤害的差值(最终伤害, 变更后最终伤害);
            Window.计算结果文本框.Text = $"属性变更后,造成的伤害:[{变更后最终伤害}]比原来的伤害:[{最终伤害}]...{结果}";
        }

        private static string 计算两次伤害的差值(double 原伤害, double 变动伤害)
        {
            string 提升百分比;
            double 提升率 = Math.Abs(((1 - (变动伤害 / 原伤害)) * 100));
            提升率 = Math.Round(提升率, 2);
            if (变动伤害 >= 原伤害)
            {
                提升百分比 = "提升" + 提升率.ToString() + "%";
            }
            else
            {
                提升百分比 = "降低" + 提升率.ToString() + "%";
            }
            return 提升百分比;
        }

        private static void 驱动盘收益计算(int 驱动盘号)
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            string 驱动盘文本 = $"{驱动盘名[驱动盘号]}{驱动盘号}\n";
            if (驱动盘主属性字典[驱动盘号][取字典键(驱动盘主属性字典[驱动盘号], 1)] < 1)
            {
                驱动盘文本 += $"{取字典键(驱动盘主属性字典[驱动盘号], 1)} : {Math.Round(驱动盘主属性字典[驱动盘号][取字典键(驱动盘主属性字典[驱动盘号], 1)] * 100, 3)}%\n\n";
            }
            else
            {
                驱动盘文本 += $"{取字典键(驱动盘主属性字典[驱动盘号], 1)} : {驱动盘主属性字典[驱动盘号][取字典键(驱动盘主属性字典[驱动盘号], 1)]}\n\n";
            }

            if (驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 0)] < 1)
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 0)} : {Math.Round(驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 0)] * 100, 3)}%\n";
            }
            else
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 0)} : {驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 0)]}\n";
            }
            if (驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 1)] < 1)
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 1)} : {Math.Round(驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 1)] * 100, 3)}%\n";
            }
            else
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 1)} : {驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 1)]}\n";
            }
            if (驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 2)] < 1)
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 2)} : {Math.Round(驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 2)] * 100, 3)}%\n";
            }
            else
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 2)} : {驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 2)]}\n";
            }
            if (驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 3)] < 1)
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 3)} : {Math.Round(驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 3)] * 100, 3)}%\n";
            }
            else
            {
                驱动盘文本 += $"{取字典键(驱动盘副属性字典[驱动盘号], 3)} : {驱动盘副属性字典[驱动盘号][取字典键(驱动盘副属性字典[驱动盘号], 3)]}\n";
            }

            double 原盘伤害 = 伤害计算();
            属性变更(驱动盘副属性字典[驱动盘号], "减");
            double 取盘伤害 = 伤害计算();

            if (驱动盘号 == 1)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(1);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘1文本框.Text = 驱动盘文本;
            }
            else if (驱动盘号 == 2)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(2);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘2文本框.Text = 驱动盘文本;
            }
            else if (驱动盘号 == 3)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(3);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘3文本框.Text = 驱动盘文本;
            }
            else if (驱动盘号 == 4)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(4);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘4文本框.Text = 驱动盘文本;
            }
            else if (驱动盘号 == 5)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(5);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘5文本框.Text = 驱动盘文本;
            }
            else if (驱动盘号 == 6)
            {
                double 换理论盘伤害 = 穷举理论满分驱动盘返回最大伤害(6);
                驱动盘文本 += $"驱动盘评分:{Math.Round(((原盘伤害 - 取盘伤害) / (换理论盘伤害 - 取盘伤害) * 100), 2)}%";
                Window.驱动盘6文本框.Text = 驱动盘文本;
            }
        }

        public static string 取字典键(Dictionary<string, dynamic> 字典, int index)
        {
            // 使用 Keys 属性获取所有键，并返回指定索引的键  
            using (var enumerator = 字典.Keys.GetEnumerator())
            {
                int currentIndex = 0; // 当前索引  
                while (enumerator.MoveNext()) // 遍历所有键  
                {
                    if (currentIndex == index) // 如果当前索引等于指定索引  
                    {
                        return enumerator.Current; // 返回当前键  
                    }
                    currentIndex++; // 增加当前索引  
                }
            }
            return "";
        }
        
        private static double 伤害计算(bool 重置 = true)
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 攻击基数 = Convert.ToDouble(Window.变更攻击基数文本框.Text);
            double 怪物防御 = Convert.ToDouble(Window.变更怪物防御文本框.Text);
            double 异常基数 = Convert.ToDouble(Window.异常基数文本框.Text);
            double 暴击率, 暴击伤害, 异常倍率 = 1;

            直伤技能倍率 = Convert.ToDouble(Window.技能倍率输入框.Text);
            double 攻击力 = 属性字典["基础攻击力"] * (1 + 属性字典["攻击力"]) + (属性字典["小攻击力"]);
            double 伤害加成乘区 = 1 + 属性字典[角色主属性];
            double 敌人防御 = 怪物防御 - 属性字典["穿透值"];
            double 敌人减防 = 1 - 属性字典["减防率"];
            double 敌人被穿透 = 1 - 属性字典["穿透率"];
            double 减防乘区 = 攻击基数 / (敌人防御 * 敌人减防 * 敌人被穿透 + 攻击基数);
            double 减抗乘区 = 1 + 属性字典["减抗率"];
            double 易伤乘区 = 1 + 属性字典["易伤率"];
            double 异常乘区 = (属性字典["异常精通"] / 100);

            if (属性字典["暴击率"] > 1)
            {
                暴击率 = 1;
            }
            else
            {
                暴击率 = 属性字典["暴击率"];
            }
            暴击伤害 = 属性字典["暴击伤害"];
            double 暴击乘区 = 1 + 暴击率 * 暴击伤害;

            double 最终伤害;
            if (角色特性 == "异常")
            {
                switch (角色主属性)
                {
                    case "物理伤害加成":
                        异常倍率 = 7.13;
                        break;
                    case "冰属性伤害加成":
                        异常倍率 = 5;
                        break;
                    case "电属性伤害加成":
                        异常倍率 = 1.25;
                        break;
                    case "火属性伤害加成":
                        异常倍率 = 0.5;
                        break;
                    case "以太伤害加成":
                        异常倍率 = 0.625;
                        break;
                }
                最终伤害 = 攻击力 * 异常乘区 * 伤害加成乘区 * 减防乘区 * 减抗乘区 * 易伤乘区 * 异常倍率 * 异常基数;
            }
            else
            {
                最终伤害 = 攻击力 * 暴击乘区 * 伤害加成乘区 * 减防乘区 * 减抗乘区 * 易伤乘区 * (直伤技能倍率 / 100);
            }
            if (重置)
            {
                保存重置属性字典("重置");
            }
            return 最终伤害;
        }

        private static double 副属性伤害计算()
        {
            MainWindow Window = System.Windows.Application.Current.MainWindow as MainWindow;
            double 攻击基数 = Convert.ToDouble(Window.变更攻击基数文本框.Text);
            double 怪物防御 = Convert.ToDouble(Window.变更怪物防御文本框.Text);
            double 异常基数 = Convert.ToDouble(Window.异常基数文本框.Text);
            double 暴击率, 暴击伤害, 异常倍率 = 1;
            
            直伤技能倍率 = Convert.ToDouble(Window.技能倍率输入框.Text);
            double 攻击力 = 副属性字典["基础攻击力"] * (1 + 副属性字典["攻击力"]) + (副属性字典["小攻击力"]);
            double 伤害加成乘区 = 1 + 副属性字典[副角色主属性];
            double 敌人防御 = 怪物防御 - 副属性字典["穿透值"];
            double 敌人减防 = 1 - 副属性字典["减防率"];
            double 敌人被穿透 = 1 - 副属性字典["穿透率"];
            double 减防乘区 = 攻击基数 / (敌人防御 * 敌人减防 * 敌人被穿透 + 攻击基数);
            double 减抗乘区 = 1 + 副属性字典["减抗率"];
            double 易伤乘区 = 1 + 副属性字典["易伤率"];
            double 异常乘区 = (副属性字典["异常精通"] / 100);
            
            if (副属性字典["暴击率"] > 1)
            {
                暴击率 = 1;
            }
            else
            {
                暴击率 = 副属性字典["暴击率"];
            }
            暴击伤害 = 副属性字典["暴击伤害"];
            double 暴击乘区 = 1 + 暴击率 * 暴击伤害;

            double 最终伤害;
            if (角色特性 == "异常")
            {
                switch (角色主属性)
                {
                    case "物理伤害加成":
                        异常倍率 = 7.13;
                        break;
                    case "冰属性伤害加成":
                        异常倍率 = 5;
                        break;
                    case "电属性伤害加成":
                        异常倍率 = 1.25;
                        break;
                    case "火属性伤害加成":
                        异常倍率 = 0.5;
                        break;
                    case "以太伤害加成":
                        异常倍率 = 0.625;
                        break;
                }
                最终伤害 = 攻击力 * 异常乘区 * 伤害加成乘区 * 减防乘区 * 减抗乘区 * 易伤乘区 * 异常倍率 * 异常基数;
            }
            else
            {
                最终伤害 = 攻击力 * 暴击乘区 * 伤害加成乘区 * 减防乘区 * 减抗乘区 * 易伤乘区 * (直伤技能倍率 / 100);
            }
            return 最终伤害;
        }
    }

    public static class 截图
    {
        public static void 游戏截图()
        {
            double 宽 = 屏幕.屏幕宽度;
            double 高 = 屏幕.屏幕高度;

            if (高 / 宽 == 0.5625)
            {
                if (屏幕.屏幕高度 > 1079)
                {
                    IntPtr 绝区零句柄 = 窗口.获取窗口句柄("绝区零");
                    if (绝区零句柄 == IntPtr.Zero)
                    {
                        绝区零句柄 = 窗口.获取窗口句柄("ZenlessZoneZero");
                    }

                    if (绝区零句柄 == IntPtr.Zero)
                    {
                        MessageBox.Show("未找到绝区零窗口,请确认游戏是否已启动!");
                        return;
                    }
                    else
                    {
                        窗口.窗口激活(绝区零句柄);
                    }
                    基础属性截图();
                    核心技截图();
                    装备截图();
                    操作.鼠标点击(1537, 1330);    //点击基础
                    操作.鼠标点击(1280, 720);     //鼠标回正
                }
                else
                {
                    MessageBox.Show("分辨率低于1080p无法运行,请修改桌面分辨率,并重启软件后再点击截图", "分辨率太低!");
                    return;
                }

            }
            else
            {
                MessageBox.Show("请将桌面和游戏分辨率都设置为16比9,如[2560x1440] [3840x2160] [1920x1080],游戏设置全屏,字体设置全局细体", "分辨率不正确!");
                return;
            }
        }

        static bool 基础属性截图()
        {
            操作.鼠标点击(1537, 1330);    //点击基础
            操作.截取图片并保存(1383, 410, 2000, 500, "角色名");    //截图角色名
            角色基础属性验证截图();
            int 计数器 = 1;
            int x = 1400;
            int y = 770;
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < 5; i++)
                {
                    操作.截取图片并保存(x, y, x + 217, y + 40, $"基础属性{计数器}");
                    计数器++;
                    操作.截取图片并保存(x + 217, y, x + 217 + 217, y + 40, $"基础属性{计数器}");
                    计数器++;
                    y += 70;
                }
                y = 770;
                x = 1895;
            }
            return true;
        }

        static void 角色基础属性验证截图()
        {
            操作.鼠标点击(2236, 686); //点击属性详情
            操作.鼠标点击(1904, 505); //点击折叠
            操作.截取图片并保存(1539, 702, 1647, 752, "角色基础1");
            操作.截取图片并保存(1539, 773, 1647, 823, "角色基础");
            操作.截取图片并保存(1539, 846, 1647, 896, "角色基础");
            操作.截取图片并保存(1539, 920, 1647, 970, "角色基础");
            操作.鼠标点击(1947, 300); //点击折叠
            跨类变量.角色基础属性计数 = 1;
        }

        static void 核心技截图()
        {
            操作.鼠标点击(1873, 1324); //点击技能
            操作.鼠标点击(2020, 400); //点击技能
            操作.截取图片并保存(152, 381, 739, 430, "核心技词条");   //核心技词条截图
            操作.鼠标点击(2295, 400); //点击技能
            操作.截取图片并保存(1028, 242, 1154, 308, "核心技等级");   //核心技等级截图
            操作.鼠标点击(137, 57); //点击返回
            return;
        }

        static void 装备截图()
        {
            操作.鼠标点击(2206, 1331);    //点击装备

            操作.截取图片并保存(1632, 527, 1654, 546, "1"); //验证驱动盘1是否佩戴,不截图
            操作.截取图片并保存(1493, 820, 1518, 843, "2"); //验证驱动盘2是否佩戴,不截图
            操作.截取图片并保存(1630, 1115, 1654, 1138, "3"); //验证驱动盘3是否佩戴,不截图
            操作.截取图片并保存(2057, 1117, 2079, 1137, "4"); //验证驱动盘4是否佩戴,不截图
            操作.截取图片并保存(2196, 822, 2219, 844, "5"); //验证驱动盘5是否佩戴,不截图
            操作.截取图片并保存(2058, 527, 2080, 548, "6"); //验证驱动盘6是否佩戴,不截图
            操作.截取图片并保存(1842, 893, 1864, 916, "7"); //验证武器是否佩戴,不截图

            驱动盘属性截图(1652, 426, "1");     //点击驱动盘1
            驱动盘属性截图(1515, 720, "2");     //点击驱动盘2
            驱动盘属性截图(1652, 1015, "3");    //点击驱动盘3
            驱动盘属性截图(2077, 1015, "4");    //点击驱动盘4
            驱动盘属性截图(2216, 720, "5");     //点击驱动盘5
            驱动盘属性截图(2077, 425, "6");     //点击驱动盘6
            武器属性截图(1870, 728);      //点击武器
            操作.鼠标点击(137, 57); //点击返回
        }

        static void 驱动盘属性截图(double x, double y, string 名)
        {
            if (跨类变量.装备[int.Parse(名)])
            {
                操作.鼠标点击(x, y);
                操作.截取图片并保存(817, 167, 1177, 214, $"驱动盘{名}_0"); //驱动盘名

                操作.截取图片并保存(835, 375, 1060, 412, $"驱动盘{名}_1");  //驱动盘主属性
                操作.截取图片并保存(1060, 375, 1285, 412, $"驱动盘{名}_2");  //驱动盘主属性

                操作.截取图片并保存(835, 500, 1060, 537, $"驱动盘{名}_3");  //驱动盘副属性1
                操作.截取图片并保存(1060, 500, 1285, 537, $"驱动盘{名}_4");  //驱动盘副属性1

                操作.截取图片并保存(835, 570, 1060, 607, $"驱动盘{名}_5");  //驱动盘副属性2
                操作.截取图片并保存(1060, 570, 1285, 607, $"驱动盘{名}_6");  //驱动盘副属性2

                操作.截取图片并保存(835, 640, 1060, 677, $"驱动盘{名}_7");  //驱动盘副属性3
                操作.截取图片并保存(1060, 640, 1285, 677, $"驱动盘{名}_8");  //驱动盘副属性3

                操作.截取图片并保存(835, 710, 1060, 747, $"驱动盘{名}_9");  //驱动盘副属性4
                操作.截取图片并保存(1060, 710, 1285, 747, $"驱动盘{名}_10");  //驱动盘副属性4
            }
        }

        static void 武器属性截图(double x, double y)
        {
            if (跨类变量.装备[7])
            {
                操作.鼠标点击(x, y);
                操作.截取图片并保存(815, 161, 1239, 219, "武器0"); //武器名

                操作.截取图片并保存(835, 445, 1060, 482, "武器1");  //武器主属性
                操作.截取图片并保存(1060, 445, 1285, 482, "武器2");  //武器主属性

                操作.截取图片并保存(835, 575, 1060, 612, "武器3");  //武器副属性
                操作.截取图片并保存(1060, 575, 1285, 612, "武器4");  //武器副属性
            }
        }
    }

    public static class 屏幕
    {
        static double 逻辑分辨率宽 = SystemParameters.PrimaryScreenWidth;
        static double 逻辑分辨率高 = SystemParameters.PrimaryScreenHeight;
        public static float 屏幕缩放比例 = GetPrimaryMonitorScaleFactor();
        public static int 屏幕宽度 = (int)(逻辑分辨率宽 * 屏幕缩放比例);
        public static int 屏幕高度 = (int)(逻辑分辨率高 * 屏幕缩放比例);

        public static float GetPrimaryMonitorScaleFactor()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                // 获取当前显示器的 DPI  
                float dpiX = g.DpiX;
                // Assuming the default DPI is 96  
                return dpiX / 96f; // 计算缩放比例  
            }
        }
    }

    public static class 窗口
    {
        // 引入 Windows API 的 FindWindow 函数
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static IntPtr 获取窗口句柄(string 标题)
        {
            IntPtr hWnd = FindWindow(null, 标题);
            if (hWnd != IntPtr.Zero)
            {
                return hWnd;
            }
            else
            {
                return IntPtr.Zero;
            }
        }


        // 引入 SetForegroundWindow 函数
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void 窗口激活(IntPtr hWnd)
        {
            // 将窗口激活
            SetForegroundWindow(hWnd);
            Thread.Sleep(500);
        }
    }

    public static class 操作
    {
        // 定义 MOUSEINPUT 结构
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        // 定义 INPUT 结构
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        // 定义常量
        private const int INPUT_MOUSE = 0;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        // 导入 SendInput 函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void 鼠标点击(double x坐标, double y坐标)
        {
            double 宽度比例 = x坐标 / 2560;
            double 高度比例 = y坐标 / 1440;

            int x = (int)(屏幕.屏幕宽度 * 宽度比例);
            int y = (int)(屏幕.屏幕高度 * 高度比例);

            int X = (x * 65535) / 屏幕.屏幕宽度;     // 计算逻辑坐标X (0 - 65535)
            int Y = (y * 65535) / 屏幕.屏幕高度;     // 计算逻辑坐标Y (0 - 65535)

            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = X;
            inputs[0].mi.dy = Y;
            inputs[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;

            // 发送鼠标移动输入
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            // 模拟左键点击
            左键单击();
        }

        static void 左键单击()
        {
            INPUT[] inputs = new INPUT[2];

            // 模拟鼠标左键按下
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

            // 模拟鼠标左键松开
            inputs[1].type = INPUT_MOUSE;
            inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;

            // 发送输入事件
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            Thread.Sleep(750);
        }

        public static void 截取图片并保存(double X1, double Y1, double X2, double Y2, string 文件名)
        {
            double x比例1 = X1 / 2560;
            double y比例1 = Y1 / 1440;
            double x比例2 = X2 / 2560;
            double y比例2 = Y2 / 1440;

            int x1 = (int)((屏幕.屏幕宽度 * x比例1));
            int y1 = (int)((屏幕.屏幕高度 * y比例1));
            int x2 = (int)((屏幕.屏幕宽度 * x比例2));
            int y2 = (int)((屏幕.屏幕高度 * y比例2));

            System.Drawing.Rectangle 坐标 = new System.Drawing.Rectangle(x1, y1, x2 - x1, y2 - y1); // 原始坐标
            Bitmap 图片 = 区域截图(坐标);

            if ("1234567".Contains(文件名))   //如果是装配未佩戴截图
            {
                for (int x = 0; x < 图片.Width; x++)  // 遍历图像的每一个像素  
                {
                    for (int y = 0; y < 图片.Height; y++)
                    {
                        Color pixelColor = 图片.GetPixel(x, y);   // 获取当前像素的颜色  
                        if (pixelColor.R >= 250 && pixelColor.G >= 250 && pixelColor.B >= 250)  // 检查是否是纯白色 
                        {
                            跨类变量.装备[int.Parse(文件名)] = true;
                            return;    //已装备
                        }
                    }
                }
                跨类变量.装备[int.Parse(文件名)] = false;
                return;    //未装备
            }

            if (文件名 == "角色基础1")
            {
                跨类变量.角色基础1图片有用 = false;
                for (int x = 0; x < 图片.Width; x++)  // 遍历图像的每一个像素  
                {
                    for (int y = 0; y < 图片.Height; y++)
                    {
                        Color pixelColor = 图片.GetPixel(x, y);   // 获取当前像素的颜色  
                        if ((pixelColor.R >= 250 && pixelColor.G >= 250 && pixelColor.B >= 250) || (pixelColor.R >= 250 && pixelColor.G == 175 && pixelColor.B == 42))  // 检查是否是纯白色或黄色 
                        {
                            文件名 = Regex.Replace(文件名, @"\d", "");
                            跨类变量.角色基础1图片有用 = true;
                        }
                    }
                }
                if (!跨类变量.角色基础1图片有用)
                {
                    return; //图片非必要属性
                }
            }

            if (文件名 == "角色基础")
            {
                文件名 = $"{文件名}{跨类变量.角色基础属性计数}";
                跨类变量.角色基础属性计数++;
                if (文件名 == "角色基础4")
                {
                    return; //图片非必要属性
                }
            }

            操作图片保存后释放资源(图片, 文件名);
            return;
        }

        public static void 操作图片保存后释放资源(Bitmap 图片, string 文件名)
        {
            if (文件名.Contains("基础属性") || 文件名.Contains("角色基础"))
            {
                bool 黄色 = false;
                for (int x = 0; x < 图片.Width; x++)  // 遍历图像的每一个像素  
                {
                    for (int y = 0; y < 图片.Height; y++)
                    {
                        Color pixelColor = 图片.GetPixel(x, y);   // 获取当前像素的颜色  
                        if (pixelColor.R == 255 && pixelColor.G == 175 && pixelColor.B == 42)  // 检查是否是黄色字体
                        {
                            黄色 = true;
                        }
                    }
                }

                if (黄色)
                {
                    图片 = 二值化处理图片(图片, "黄");
                }
                else
                {
                    图片 = 二值化处理图片(图片);
                }
            }
            else
            {
                图片 = 二值化处理图片(图片);
            }

            // 找到白字的边界矩形
            System.Drawing.Rectangle textBounds = 取白字边界(图片);

            // 在文字周围扩展一像素
            textBounds.Inflate(1, 1);

            // 验证Rectangle是否合法
            if (!验算坐标是否合法(textBounds, 图片.Size))
            {
                跨类变量.截图结果 = false;
            }

            Console.WriteLine(textBounds);
            try
            {
                // 裁剪图像
                图片 = 裁剪图像(图片, textBounds);
            }
            catch (Exception)
            {
                if (文件名.Contains("_9") || 文件名.Contains("_10"))  //驱动盘不存在第四词条
                {
                    跨类变量.截图结果 = true;   //无第四词条不影响最终结果
                    return;
                }
                else
                {
                    图片.Save($"./py/img/{文件名}错误图片!.png", ImageFormat.Png);   // 保存错误截图
                    throw new InvalidOperationException("操作无效，因为某些条件未满足。");

                }
            }

            // 创建新的 Bitmap,长度扩展30,高度扩展18
            Bitmap 新画板 = new Bitmap(
                图片.Width + 2 * 24,
                图片.Height + 2 * 9
            );

            // 在新 Bitmap 上绘制黑色背景  
            using (Graphics g = Graphics.FromImage(新画板))
            {
                // 填充黑色背景  
                g.Clear(Color.Black);

                // 将原始 Bitmap 绘制到扩展后的 Bitmap 中  
                g.DrawImage(图片, new Rectangle(15, 9, 图片.Width, 图片.Height));
            }
            新画板 = 调整图片高度(新画板, 48);
            新画板.Save($"./py/img/{文件名}.png", ImageFormat.Png);   // 保存截图
            图片.Dispose();
            新画板.Dispose();
            跨类变量.截图结果 = true;
        }

        public static Bitmap 调整图片高度(Bitmap 图片, int 高度)
        {
            // 获取原始图片的宽度和高度
            int 图片宽 = 图片.Width;
            int 图片高 = 图片.Height;

            // 计算新的宽度，保持比例不变
            int newWidth = (int)((double)图片宽 / 图片高 * 高度);

            // 创建一个新的Bitmap对象，用于保存调整后的图片
            using (Bitmap resizedImage = new Bitmap(newWidth, 高度))
            {
                // 创建Graphics对象，用于绘制新图片
                using (Graphics graphics = Graphics.FromImage(resizedImage))
                {
                    // 设置高质量插值模式
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    // 绘制调整后的图片
                    graphics.DrawImage(图片, 0, 0, newWidth, 高度);
                }

                // 返回调整后的Bitmap对象
                return new Bitmap(resizedImage);
            }
        }

        public static bool 验算坐标是否合法(System.Drawing.Rectangle rectangle, System.Drawing.Size imageSize)
        {
            // 检查左上角坐标
            if (rectangle.X < 0 || rectangle.Y < 0)
            {
                Console.WriteLine("图片不合法,x和y小于0");
                return false;
            }
            // 检查右下角坐标
            if (rectangle.Right > imageSize.Width || rectangle.Bottom > imageSize.Height)
            {
                Console.WriteLine("图片不合法,右下角坐标超界");
                return false;
            }
            // 检查宽度和高度是否为正数
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
            {
                Console.WriteLine("图片不合法,宽高小于0");
                return false;
            }
            return true;
        }

        public static Bitmap 区域截图(System.Drawing.Rectangle region)
        {
            Bitmap bitmap = new Bitmap(region.Width, region.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 从指定区域拷贝屏幕内容
                g.CopyFromScreen(region.Location, System.Drawing.Point.Empty, region.Size);
            }
            return bitmap;
        }

        static System.Drawing.Rectangle 取白字边界(Bitmap bitmap)
        {
            int minX = bitmap.Width, minY = bitmap.Height;
            int maxX = 0, maxY = 0;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // 检查像素是否为白色
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // 创建最小边界矩形
            return new System.Drawing.Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        static Bitmap 裁剪图像(Bitmap bitmap, System.Drawing.Rectangle cropArea)
        {
            // 根据指定区域裁剪图片
            return bitmap.Clone(cropArea, bitmap.PixelFormat);
        }

        private static Bitmap 二值化处理图片(Bitmap image, string 颜色 = "白")
        {
            Bitmap binarized = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    // 计算像素的灰度值
                    int gray = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    // 选择黑色或白色
                    Color binarizedColor;
                    if (颜色 == "白")
                    {
                        binarizedColor = gray < 220 ? Color.Black : Color.White;
                    }
                    else
                    {
                        binarizedColor = gray < 145 ? Color.Black : Color.White;
                    }
                    binarized.SetPixel(x, y, binarizedColor);
                }
            }
            return binarized;
        }
    }

    public static class 跨类变量
    {
        public static bool 截图结果 = true;
        public static bool[] 装备 = new bool[8];
        public static bool 是否写好主属性 = false;
        public static int 角色基础属性计数 = 1;
        public static bool 角色基础1图片有用 = false;
    }
}