using System;
using System.Net;
using System.Text;
using System.Threading;

namespace LibraryThing.WebServices.Tests
{
    public class FakeWebServer : IDisposable
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
        private readonly string _contentType;

        public FakeWebServer(string[] prefixes, Func<HttpListenerRequest, string> method, string contentType = "application/json")
        {
            _contentType = contentType;

            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Must have Windows XP SP2/Server 2003 or later.");
            }

            if (prefixes == null)
            {
                throw new ArgumentNullException(nameof(prefixes));
            }
            else if (prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            foreach (string prefix in prefixes)
            {
                _listener.Prefixes.Add(prefix);
            }

            _responderMethod = method;
            _listener.Start();
        }

        public FakeWebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method)
        {
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Fake Webserver Running...");

                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(c =>
                        {
                            var context = c as HttpListenerContext;

                            try
                            {
                                if (context == null) return;

                                var response = _responderMethod(context.Request);
                                var buffer = Encoding.UTF8.GetBytes(response);
                                context.Response.ContentLength64 = buffer.Length;
                                context.Response.ContentType = _contentType;
                                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                            }
                            catch
                            {
                                // ignored
                            }
                            finally
                            {
                                context?.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            });
        }

        public void Dispose()
        {
            Stop();
        }

        private void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
