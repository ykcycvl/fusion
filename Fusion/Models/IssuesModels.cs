using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fusion.Models
{
    public class IssuesModels
    {
        public class Result
        {
            public bool Success;
            public string Message;
        }

        public class IssueModel
        {
            private Entities db = new Entities();
            public int? id { get; set; }
            [Display(Name="Тема")]
            public string Title { get; set; }
            [Display(Name = "Содержание (описание)")]
            public string Content { get; set; }
            [Display(Name = "Тэги")]
            public string Tags { get; set; }
            [Display(Name = "Автор")]
            public string Author { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastModified { get; set; }
            public string LastEditor { get; set; }

            public Result Save()
            {
                Result result = new Result() { Success = false, Message = "" };

                try
                {
                    if (this.id == null)
                    {
                        db.Issue.Add(new Issue()
                        {
                            Author = this.Author,
                            Created = this.Created,
                            IssueContent = this.Content,
                            LastEditor = this.LastEditor,
                            LastModified = this.LastModified,
                            Tags = this.Tags,
                            Title = this.Title
                        });

                        db.SaveChanges();
                    }
                    else
                    {
                        var issue = db.Issue.FirstOrDefault(p => p.id == this.id);

                        if (issue != null)
                        {
                            issue.IssueContent = this.Content;
                            issue.LastEditor = this.LastEditor;
                            issue.LastModified = this.LastModified;
                            issue.Tags = this.Tags;
                            issue.Title = this.Title;
                        }

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }

                return result;
            }
            public void Get(int id)
            {
                var issue = db.Issue.FirstOrDefault(p => p.id == id);

                if (issue != null)
                {
                    this.Author = issue.Author;
                    this.Content = issue.IssueContent;
                    this.Created = issue.Created;
                    this.id = issue.id;
                    this.LastEditor = issue.LastEditor;
                    this.LastModified = issue.LastModified;
                    this.Tags = issue.Tags;
                    this.Title = issue.Title;
                }
            }
            public List<Issue> List()
            {
                return db.Issue.ToList();
            }
        }
    }
}