using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Xunit;

namespace CsProjEditor.Tests
{
    public class GroupTests
    {
        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj",
            new[] { "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup" },
            new[] { "Import", "Import", "PropertyGroup", "PropertyGroup", "ItemGroup", "ItemGroup", "ItemGroup", "PropertyGroup", "Import", "Target", "Target", "PropertyGroup" })]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj",
            new[] { "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup" },
            new[] { "Import", "Import", "PropertyGroup", "PropertyGroup", "ItemGroup", "ItemGroup", "ItemGroup", "PropertyGroup", "Import", "Target", "Target", "PropertyGroup" })]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj",
            new[] { "PropertyGroup" },
            new[] { "PropertyGroup", "ItemGroup", "ItemGroup", "Target", "Target"})]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj",
            new[] { "PropertyGroup" },
            new[] { "PropertyGroup", "ItemGroup", "ItemGroup", "Target", "Target" })]
        public void GetTest(string csprojPath, string[] expected, string[] expected2)
        {
            var csproj = Project.Load(csprojPath);
            csproj.GetGroup("PropertyGroup").Should().BeEquivalentTo(expected);

            csproj.GetGroups().Should().BeEquivalentTo(expected2);
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void GetFailTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);
            csproj.GetGroup("PropertyGroupX").Should().BeEmpty();
            csproj.GetGroup("ItemGroupX").Should().BeEmpty();
            csproj.GetGroup("TargetX").Should().BeEmpty();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void ExistsTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);
            csproj.ExistsGroup("PropertyGroup").Should().BeTrue();
            csproj.ExistsGroup("ItemGroup").Should().BeTrue();
            csproj.ExistsGroup("Target").Should().BeTrue();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void ExistsFailTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);
            csproj.ExistsGroup("PropertyGroupX").Should().BeFalse();
            csproj.ExistsGroup("ItemGroupX").Should().BeFalse();
            csproj.ExistsGroup("TargetX").Should().BeFalse();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj", new[] { "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup" })]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj", new[] { "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup", "PropertyGroup" })]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj", new[] { "PropertyGroup", "PropertyGroup" })]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj", new[] { "PropertyGroup", "PropertyGroup" })]
        public void InsertTest(string csprojPath, string[] expected)
        {
            var csproj = Project.Load(csprojPath);
            // additional existing group
            csproj.ExistsGroup("PropertyGroup").Should().BeTrue();
            csproj.InsertGroup("PropertyGroup");
            csproj.GetGroup("PropertyGroup").Should().BeEquivalentTo(expected);

            // new group
            csproj.ExistsGroup("Test").Should().BeFalse();
            csproj.InsertGroup("Test");
            csproj.ExistsGroup("Test").Should().BeTrue();
            csproj.GetGroup("Test").Should().BeEquivalentTo("Test");
            csproj.InsertNode("Test", "Hoge", "Value");
            csproj.ExistsNodeValue("Test", "Hoge", "Value").Should().BeTrue();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void InsertFailTest(string csprojPath)
        {
            // no test.
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void ReplaceTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);
            // simple replacement
            csproj.ExistsGroup("PropertyGroup").Should().BeTrue();
            csproj.ReplaceGroup("PropertyGroup", "Hogemoge");
            csproj.ExistsGroup("Hogemoge").Should().BeTrue();

            // replacement can specify which letter to replace with via `pattern`.
            // In this case, node name `ProjectGuid` will replace `Guid` with `Hogemoge`, so the resuld must be `ProjectHogemoge`.
            csproj.ExistsGroup("ItemGroup").Should().BeTrue();
            csproj.ReplaceGroup("ItemGroup", "Item", "Piyo");
            csproj.ExistsGroup("PiyoGroup").Should().BeTrue();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void ReplaceFailTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);

            // not exists node will not do any.
            csproj.ExistsGroup("HogemogeGroup").Should().BeFalse();
            csproj.ReplaceGroup("HogemogeGroup", "FugaFuga");
            csproj.ExistsGroup("FugaFuga").Should().BeFalse();

            csproj.ExistsGroup("HogemogeGroup").Should().BeFalse();
            csproj.ReplaceGroup("HogemogeGroup", "Hogemoge", "FugaFuga");
            csproj.ExistsGroup("HogemogeGroup").Should().BeFalse();
            csproj.ExistsGroup("FugaFugaGroup").Should().BeFalse();

            csproj.ExistsGroup("ItemFoo").Should().BeFalse();
            csproj.ReplaceGroup("ItemFoo", "Hoge", "Fuga");
            csproj.ExistsGroup("ItemFoo").Should().BeFalse();
            csproj.ExistsGroup("Hoge").Should().BeFalse();
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void RemoveTest(string csprojPath)
        {
            // CRLF test will only run on windows
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && csprojPath.Contains("CRLF"))
                return;

            var csproj = Project.Load(csprojPath);
            csproj.InsertGroup("Test");
            csproj.InsertNode("Test", "Foo", "value");

            // Remove with leave blank line test.
            var beforeCount = csproj.Root.ToString().Split(csproj.Eol.ToEolString()).Length;
            csproj.ExistsGroup("Test").Should().BeTrue();
            var nodeCounts = csproj.GetNodes("Test").Length;
            csproj.RemoveGroup("Test", true);
            csproj.ExistsGroup("Test").Should().BeFalse();
            var afterCount = csproj.Root.ToString().Split(csproj.Eol.ToEolString()).Length;
            afterCount.Should().Be(beforeCount - nodeCounts - 1);

            // Remove node and remove blank line test.
            csproj.InsertGroup("Test");
            csproj.InsertNode("Test", "Foo", "value");
            csproj.ExistsGroup("Test").Should().BeTrue();
            csproj.RemoveGroup("Test");
            csproj.ExistsGroup("Test").Should().BeFalse();
            // remove blankline will remove all blank line. In this case, previous remove's blank line also removed.
            csproj.Root.ToString().Split(csproj.Eol.ToEolString()).Length.Should().Be(beforeCount - 1 - 1 - 1);
        }

        [Theory]
        [InlineData("testdata/SimpleOldCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleOldCsProjUtf8_LF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_CRLF.csproj")]
        [InlineData("testdata/SimpleNewCsProjUtf8_LF.csproj")]
        public void RemoveFailTest(string csprojPath)
        {
            var csproj = Project.Load(csprojPath);

            // not exists node will not do any.
            csproj.ExistsGroup("Test").Should().BeFalse();
            csproj.RemoveGroup("Test", true);
        }
    }
}
