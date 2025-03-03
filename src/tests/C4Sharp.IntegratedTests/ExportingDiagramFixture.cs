using System.IO;
using FluentAssertions;

namespace C4Sharp.IntegratedTests
{
    public class ExportingDiagramFixture
    {
        protected void Setup(string path = "c4")
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        protected static void CleanUp(string path = "c4")
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        protected static void VerifyIfResourceFilesExists(string path = "c4")
        {
            var files = new[]
            {
                Path.Join(path, "resources", "C4.puml"),
                Path.Join(path, "resources", "C4_Component.puml"),
                Path.Join(path, "resources", "C4_Context.puml"),
                Path.Join(path, "resources", "C4_Container.puml"),
                Path.Join(path, "resources", "C4_Deployment.puml"),
            };

            VerifyIfFilesExists(files);
        }

        protected static void VerifyIfPumlFilesExists(string diagramName, string path = "c4")
        {
            var files = new[]
            {
                Path.Join(path, $"{diagramName}-c4component.puml"),
                Path.Join(path, $"{diagramName}-c4context.puml"),
                Path.Join(path, $"{diagramName}-c4container.puml"),
                Path.Join(path, $"{diagramName}-c4deployment.puml")
            };

            VerifyIfFilesExists(files);
        }

        protected static void VerifyIfPngFilesExists(string diagramName, string path = "c4")
        {
            var files = new[]
            {
                Path.Join(path, $"{diagramName}-c4component.png"),
                Path.Join(path, $"{diagramName}-c4context.png"),
                Path.Join(path, $"{diagramName}-c4container.png"),
                Path.Join(path, $"{diagramName}-c4deployment.png")
            };

            VerifyIfFilesExists(files);
        }
        
        protected static void VerifyIfPngFilesNonExists(string diagramName, string path = "c4")
        {
            var files = new[]
            {
                Path.Join(path, $"{diagramName}-c4component.png"),
                Path.Join(path, $"{diagramName}-c4context.png"),
                Path.Join(path, $"{diagramName}-c4container.png"),
                Path.Join(path, $"{diagramName}-c4deployment.png")
            };

            VerifyIfFilesNonExists(files);
        }

        private static void VerifyIfFilesExists(params string[] path)
        {
            foreach (var file in path)
            {
                File.Exists(file).Should().BeTrue();
            }
        }
        
        private static void VerifyIfFilesNonExists(params string[] path)
        {
            foreach (var file in path)
            {
                File.Exists(file).Should().BeFalse();
            }
        }        
    }
}