using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Services.Description;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace Utility
{
    public class WebServiceProxy
    {
        protected static object _obj = new object();
        protected static Object _proxy = null;
        public static Object Instance
        {
            get
            {
                if (_proxy == null)
                {
                    lock (_obj)
                    {
                        _proxy = Create();
                    }
                }
                return _proxy;
            }
            set { _proxy = value; }
        }

        protected static string GetUrl(string name )
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            System.Configuration.AppSettingsSection section = config.AppSettings;
                    
            if (section != null)
            {
                foreach (System.Configuration.KeyValueConfigurationElement  item in section.Settings)
                {
                   string sname = item.Key;
                    string address = item.Value;
                    if (sname.ToLower() == name.ToLower())
                    {
                        return address;
                    }
                }
            }
            return "";
        }

        protected static string GetUrlFromIni()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\Config.ini";
            string url = IniUtil.ReadIniValue(path, "CallNumberService", "Url");
            string port = IniUtil.ReadIniValue(path, "CallNumberService", "Port");
            return string.Format("http://{0}:{1}/TCHIS/CallNumberService/CallNumberWebService.asmx", url, port);
        }

        protected static object Create()
        {
            string url = GetUrlFromIni(); // GetUrl("callnumberurl");
            return Create(url, "TCCallNumberService", null);
        }

        protected static object Create(string url , string codespace , string classname  )
        {
            try
            {
                Uri uri = new Uri(url);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                throw new Exception ("叫号服务地址错误，请确认配置正确!");
            }

            if ( string.IsNullOrEmpty( classname ))
            {
                classname = WebServiceProxy.GetWsClassName(url);
            }
            try
            {
                   //获取WSDL
                 WebClient wc = new WebClient();
                 Stream stream = wc.OpenRead(url+"?WSDL");
                 ServiceDescription sd  = ServiceDescription.Read(stream);
                 stream.Close();
                 stream = null;
                 ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                 sdi.AddServiceDescription(sd, "", "");
                 CodeNamespace cn = new CodeNamespace(codespace);
                  //生成客户端代理类代码
                 CodeCompileUnit ccu = new CodeCompileUnit();
                 ccu.Namespaces.Add(cn);
                 sdi.Import(cn ,ccu); 
                 CSharpCodeProvider csc = new CSharpCodeProvider();
                 //ICodeCompiler icc = csc.CreateCompiler();      
                 //设定编译参数
                 CompilerParameters cplist  = new CompilerParameters();
                 cplist.GenerateExecutable  = false;
                 cplist.GenerateInMemory = true;
                 cplist.ReferencedAssemblies.Add("System.dll");
                 cplist.ReferencedAssemblies.Add("System.XML.dll");
                 cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                 cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类
                 CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu); //icc.CompileAssemblyFromDom(cplist, ccu);
                 if (true == cr.Errors.HasErrors)
                 {
                     System.Text.StringBuilder sb = new System.Text.StringBuilder();
                     foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                     {
                         sb.Append(ce.ToString());
                         sb.Append(System.Environment.NewLine);
                     }
                     throw new Exception(sb.ToString());
                 } 
                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(codespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                //System.Reflection.MethodInfo mi = t.GetMethod(methodName);
               // return mi.Invoke(obj, args);
                return obj;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
                }
                else
                {
                    throw ex;
                }
            }
        }

        public static object Invoke( string method , string content)
        {
            Type t = Instance.GetType();
            MethodInfo methodinfo = t.GetMethod(method );
            return methodinfo.Invoke(Instance, new object[] { content });
        }

        public static object Invoke(string url, string method, string content)
        {            
            _proxy = Create(url, "TCCallNumberService", null);
            Type t = Instance.GetType();
            MethodInfo methodinfo = t.GetMethod(method);
            return methodinfo.Invoke(Instance, new object[] { content });
        }

         protected static object Invoke( string url, string codespace , string classname , string methodName, object[] args)
         {
             if ((classname == null) || (classname == ""))
             {
                 classname = WebServiceProxy.GetWsClassName(url);
             }
              try
             {
                //获取WSDL
                 WebClient wc                   = new WebClient();
                 Stream stream                  = wc.OpenRead(url+"?WSDL");
                 ServiceDescription sd          = ServiceDescription.Read(stream);
                 stream.Close();
                 stream = null;

                 ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                 sdi.AddServiceDescription(sd,"","");
                
                 CodeNamespace cn = new CodeNamespace(codespace);
                 //生成客户端代理类代码
                 CodeCompileUnit ccu             = new CodeCompileUnit();
                 ccu.Namespaces.Add(cn);
                 sdi.Import(cn ,ccu); 
                 CSharpCodeProvider csc          = new CSharpCodeProvider();
                 //ICodeCompiler icc               = csc.CreateCompiler();
                  //设定编译参数
                 CompilerParameters cplist       = new CompilerParameters();
                 cplist.GenerateExecutable       = false;
                 cplist.GenerateInMemory         = true;
                 cplist.ReferencedAssemblies.Add("System.dll");
                 cplist.ReferencedAssemblies.Add("System.XML.dll");
                 cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                 cplist.ReferencedAssemblies.Add("System.Data.dll");
                   //编译代理类
                 CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);//icc.CompileAssemblyFromDom(cplist, ccu);
                if(true == cr.Errors.HasErrors)
                 {
                     System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach(System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                     {
                         sb.Append(ce.ToString());
                         sb.Append(System.Environment.NewLine);
                     }
                    throw new Exception(sb.ToString());
                 } 
                  //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(codespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodName);

                return mi.Invoke(obj, args);
             }
              catch (Exception ex)
              {
                  throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
              }
         }
    
        private static string GetWsClassName(string wsUrl)
         {
             string[] parts = wsUrl.Split('/');
             string[] pps = parts[parts.Length - 1].Split('.');

             return pps[0];
         }
    }
}
