﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Collections.Immutable
Imports Microsoft.CodeAnalysis.Classification
Imports Microsoft.CodeAnalysis.QuickInfo
Imports Microsoft.CodeAnalysis.Tags
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.VisualStudio.Core.Imaging
Imports Microsoft.VisualStudio.Imaging
Imports Microsoft.VisualStudio.Text.Adornments
Imports QuickInfoItem = Microsoft.CodeAnalysis.QuickInfo.QuickInfoItem

Namespace Microsoft.CodeAnalysis.Editor.UnitTests.IntelliSense
    Public Class IntellisenseQuickInfoBuilderTests
        Inherits AbstractIntellisenseQuickInfoBuilderTests

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33001, "https://github.com/dotnet/roslyn/issues/33001")>
        Public Async Sub BuildQuickInfoItem()

            Dim codeAnalysisQuickInfoItem =
                QuickInfoItem.Create(
                    New TextSpan(0, 0),
                    ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Public),
                    ImmutableArray.Create(
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Description,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Keyword, "void"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Class, "Console"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Method, "WriteLine"),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Keyword, "string"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Parameter, "value"),
                                New TaggedText(TextTags.Punctuation, ")"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Punctuation, "+"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "18"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "overloads"),
                                New TaggedText(TextTags.Punctuation, ")"))),
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.DocumentationComments,
                            ImmutableArray.Create(New TaggedText(TextTags.Text, "Writes the specified string value, followed by the current line terminator, to the standard output stream."))),
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Exception,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Text, "Exceptions"),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Namespace, "System"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Namespace, "IO"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Class, "IOException")))))

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(codeAnalysisQuickInfoItem)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPublic)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "Console"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "WriteLine"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "string"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "value"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "+"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "18"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "overloads"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Writes the specified string value, followed by the current line terminator, to the standard output stream."))),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Exceptions")),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "System"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "IO"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.ClassName, "IOException"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33001, "https://github.com/dotnet/roslyn/issues/33001")>
        Public Async Sub BuildQuickInfoItemWithoutDocumentation()

            Dim codeAnalysisQuickInfoItem =
                QuickInfoItem.Create(
                    New TextSpan(0, 0),
                    ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Public),
                    ImmutableArray.Create(
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Description,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Keyword, "void"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Class, "Console"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Method, "WriteLine"),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Keyword, "string"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Parameter, "value"),
                                New TaggedText(TextTags.Punctuation, ")"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Punctuation, "+"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "18"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "overloads"),
                                New TaggedText(TextTags.Punctuation, ")"))),
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Exception,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Text, "Exceptions"),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Namespace, "System"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Namespace, "IO"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Class, "IOException")))))

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(codeAnalysisQuickInfoItem)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Wrapped,
                    New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPublic)),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.ClassName, "Console"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.MethodName, "WriteLine"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "string"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "value"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "+"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "18"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "overloads"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Exceptions")),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "System"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "IO"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.ClassName, "IOException"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33001, "https://github.com/dotnet/roslyn/issues/33001")>
        Public Async Sub BuildQuickInfoItemWithMultiLineDocumentation()

            Dim codeAnalysisQuickInfoItem =
                QuickInfoItem.Create(
                    New TextSpan(0, 0),
                    ImmutableArray.Create(WellKnownTags.Method, WellKnownTags.Public),
                    ImmutableArray.Create(
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Description,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Keyword, "void"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Class, "Console"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Method, "WriteLine"),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Keyword, "string"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Parameter, "value"),
                                New TaggedText(TextTags.Punctuation, ")"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Punctuation, "("),
                                New TaggedText(TextTags.Punctuation, "+"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "18"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Text, "overloads"),
                                New TaggedText(TextTags.Punctuation, ")"))),
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.DocumentationComments,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Text, "Documentation line 1."),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Text, "Documentation line 2."),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Text, "Documentation paragraph 2."),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Text, "Documentation paragraph 2 line 2."),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Text, "Documentation paragraph 3."))),
                        QuickInfoSection.Create(
                            QuickInfoSectionKinds.Exception,
                            ImmutableArray.Create(
                                New TaggedText(TextTags.Text, "Exceptions"),
                                New TaggedText(TextTags.LineBreak, "\r\n"),
                                New TaggedText(TextTags.Space, " "),
                                New TaggedText(TextTags.Namespace, "System"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Namespace, "IO"),
                                New TaggedText(TextTags.Punctuation, "."),
                                New TaggedText(TextTags.Class, "IOException")))))

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(codeAnalysisQuickInfoItem)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPublic)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "Console"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "WriteLine"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "string"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "value"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "+"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "18"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "overloads"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ContainerElement(
                        ContainerElementStyle.Stacked,
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation line 1.")),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation line 2.")))),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 2.")),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 2 line 2."))),
                New ClassifiedTextElement(
                    New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 3.")),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Exceptions")),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "System"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "IO"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.ClassName, "IOException"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33001, "https://github.com/dotnet/roslyn/issues/33001")>
        Public Async Sub BuildQuickInfoFromSymbol()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.IO;
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// Documentation line 1.&lt;br/&gt;
                                /// Documentation line 2.
                                /// &lt;para&gt;Documentation paragraph 2.&lt;br/&gt;
                                /// Documentation paragraph 2
                                /// line 2.&lt;/para&gt;
                                /// &lt;para&gt;Documentation paragraph 3.&lt;/para&gt;
                                /// &lt;/summary&gt;
                                /// &lt;exception cref="IOException"&gt;If something fails&lt;/exception&gt;
                                void MyMethod(CancellationToken cancellationToken = default(CancellationToken)) {
                                    MyM$$ethod();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "["),
                            New ClassifiedTextRun(ClassificationTypeNames.StructName, "CancellationToken"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "cancellationToken"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "="),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "default"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "]"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ContainerElement(
                        ContainerElementStyle.Stacked,
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation line 1.")),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation line 2.")))),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 2.")),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 2 line 2."))),
                New ClassifiedTextElement(
                    New ClassifiedTextRun(ClassificationTypeNames.Text, "Documentation paragraph 3.")),
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, FeaturesResources.Exceptions_colon)),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, "  "),
                        New ClassifiedTextRun(ClassificationTypeNames.ClassName, "IOException"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfTheory, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <InlineData("<para>text1</para><para>text2</para>")>
        <InlineData("text1<br/><br/>text2")>
        <InlineData("text1<br/><br/><br/>text2")>
        <InlineData("<br/>text1<br/><br/>text2<br/>")>
        <InlineData("<br/><br/>text1<br/><br/>text2<br/><br/>")>
        <InlineData("<para>text1<br/><br/>text2</para>")>
        <InlineData("<para/>text1<br/><br/>text2<para/>")>
        <InlineData("<para/><para/>text1<br/><br/>text2<para/><para/>")>
        <InlineData("<para>text1</para><br/><para>text2</para>")>
        <WorkItem(33001, "https://github.com/dotnet/roslyn/issues/33001")>
        Public Async Sub EquivalentParagraphForms(summary As String)
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.IO;
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// <%= summary %>
                                /// &lt;/summary&gt;
                                void MyMethod() {
                                    MyM$$ethod();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "text1"))),
                New ClassifiedTextElement(
                    New ClassifiedTextRun(ClassificationTypeNames.Text, "text2")))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        Public Async Sub InlineCodeElement()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.IO;
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// This method returns &lt;c&gt;true&lt;/c&gt;.
                                /// &lt;/summary&gt;
                                bool MyMethod() {
                                    return MyM$$ethod();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "bool"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "This method returns"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "true", ClassifiedTextRunStyle.UseClassificationFont),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "."))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        Public Async Sub BlockLevelCodeElement()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.IO;
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// This method returns &lt;code&gt;true&lt;/code&gt;.
                                /// &lt;/summary&gt;
                                bool MyMethod() {
                                    return MyM$$ethod();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "bool"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "This method returns"))),
                New ClassifiedTextElement(
                    New ClassifiedTextRun(ClassificationTypeNames.Text, "true", ClassifiedTextRunStyle.UseClassificationFont)),
                New ClassifiedTextElement(
                    New ClassifiedTextRun(ClassificationTypeNames.Text, ".")))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33546, "https://github.com/dotnet/roslyn/issues/33546")>
        Public Async Sub QuickInfoForParameterReference()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// The parameter is &lt;paramref name="p"/&gt;.
                                /// &lt;/summary&gt;
                                void MyMethod(CancellationToken p) {
                                    MyM$$ethod(CancellationToken.None);
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.StructName, "CancellationToken"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "p"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "The parameter is"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.ParameterName, "p"),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "."))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33546, "https://github.com/dotnet/roslyn/issues/33546")>
        Public Async Sub QuickInfoForReadOnlyMethodReference()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            struct MyStruct {
                                readonly void MyMethod() {
                                    MyM$$ethod();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Wrapped,
                    New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "readonly"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.StructName, "MyStruct"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33546, "https://github.com/dotnet/roslyn/issues/33546")>
        Public Async Sub QuickInfoForReadOnlyPropertyReference()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            struct MyStruct {
                                readonly int MyProperty => My$$Property;
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Wrapped,
                    New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.PropertyPrivate)),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "readonly"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "int"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.StructName, "MyStruct"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.PropertyName, "MyProperty"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "{"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "get"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ";"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "}"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33546, "https://github.com/dotnet/roslyn/issues/33546")>
        Public Async Sub QuickInfoForReadOnlyEventReference()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            struct MyStruct {
                                readonly event System.Action MyEvent { add { My$$Event += value; } remove { } }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Wrapped,
                    New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.EventPrivate)),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Keyword, "readonly"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.NamespaceName, "System"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.DelegateName, "Action"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.StructName, "MyStruct"),
                        New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                        New ClassifiedTextRun(ClassificationTypeNames.EventName, "MyEvent"))))

            AssertEqualAdornments(expected, container)
        End Sub

        <WpfFact, Trait(Traits.Feature, Traits.Features.QuickInfo)>
        <WorkItem(33546, "https://github.com/dotnet/roslyn/issues/33546")>
        Public Async Sub QuickInfoForTypeParameterReference()
            Dim workspace =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Document>
                            using System.Threading;
                            class MyClass {
                                /// &lt;summary&gt;
                                /// The type parameter is &lt;typeparamref name="T"/&gt;.
                                /// &lt;/summary&gt;
                                void MyMethod&lt;T&gt;() {
                                    MyM$$ethod&lt;int&gt;();
                                }
                            }
                        </Document>
                    </Project>
                </Workspace>

            Dim intellisenseQuickInfo = Await GetQuickInfoItemAsync(workspace, LanguageNames.CSharp)
            Assert.NotNull(intellisenseQuickInfo)

            Dim container = Assert.IsType(Of ContainerElement)(intellisenseQuickInfo.Item)

            Dim expected = New ContainerElement(
                ContainerElementStyle.Stacked Or ContainerElementStyle.VerticalPadding,
                New ContainerElement(
                    ContainerElementStyle.Stacked,
                    New ContainerElement(
                        ContainerElementStyle.Wrapped,
                        New ImageElement(New ImageId(KnownImageIds.ImageCatalogGuid, KnownImageIds.MethodPrivate)),
                        New ClassifiedTextElement(
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "void"),
                            New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                            New ClassifiedTextRun(ClassificationTypeNames.ClassName, "MyClass"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "."),
                            New ClassifiedTextRun(ClassificationTypeNames.MethodName, "MyMethod"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "<"),
                            New ClassifiedTextRun(ClassificationTypeNames.Keyword, "int"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ">"),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, "("),
                            New ClassifiedTextRun(ClassificationTypeNames.Punctuation, ")"))),
                    New ClassifiedTextElement(
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "The type parameter is"),
                        New ClassifiedTextRun(ClassificationTypeNames.WhiteSpace, " "),
                        New ClassifiedTextRun(ClassificationTypeNames.TypeParameterName, "T"),
                        New ClassifiedTextRun(ClassificationTypeNames.Text, "."))))

            AssertEqualAdornments(expected, container)
        End Sub
    End Class
End Namespace
