using Common.Configurations;
using EntityFrameworkExample.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Utils
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext() { }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public virtual DbSet<ApiKey> ApiKey { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<DevInfo> DevInfo { get; set; }
        public virtual DbSet<FailReason> FailReason { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RelFailReasonTest> RelFailReasonTest { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<Variant> Variant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.ConnectionString, x => x.ServerVersion("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiKey>(entity =>
            {
                entity.ToTable("api_key");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)")
                    .HasComment("API key which available for writing test info")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.KeyInfo)
                    .IsRequired()
                    .HasColumnName("key_info")
                    .HasColumnType("varchar(10000)")
                    .HasComment("Key info (external resource name project name, whatever)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("attachment");

                entity.HasIndex(e => e.TestId)
                    .HasName("test_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasComment("Content in base64");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasColumnType("varchar(255)")
                    .HasComment("Content type (255 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.TestId)
                    .HasColumnName("test_id")
                    .HasComment("Test ID (test.id)");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("attachment_ibfk_1");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.HasIndex(e => e.Login)
                    .HasName("author_login_u")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Author email")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Author login")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Author name")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<DevInfo>(entity =>
            {
                entity.ToTable("dev_info");

                entity.HasIndex(e => e.TestId)
                    .HasName("test_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DevTime)
                    .HasColumnName("dev_time")
                    .HasComment("Test development time");

                entity.Property(e => e.TestId)
                    .HasColumnName("test_id")
                    .HasComment("Test ID (test.id)");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.DevInfo)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dev_info_ibfk_1");
            });

            modelBuilder.Entity<FailReason>(entity =>
            {
                entity.ToTable("fail_reason");

                entity.HasIndex(e => e.Name)
                    .HasName("fail_reason_name_u")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsGlobal)
                    .HasColumnName("is_global")
                    .HasComment("Is current reason global for all projects?");

                entity.Property(e => e.IsSession)
                    .HasColumnName("is_session")
                    .HasComment("Is current reason available for session?");

                entity.Property(e => e.IsStatsIgnored)
                    .HasColumnName("is_stats_ignored")
                    .HasComment("Is current reason will be ignored in statistic count?");

                entity.Property(e => e.IsTest)
                    .HasColumnName("is_test")
                    .HasComment("Is current reason available for test?");

                entity.Property(e => e.IsUnchangeable)
                    .HasColumnName("is_unchangeable")
                    .HasComment("Is current reason cant be changed to other reason?");

                entity.Property(e => e.IsUnremovable)
                    .HasColumnName("is_unremovable")
                    .HasComment("Is current reason cant be deleted?");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Fail reason name (1000 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.HasIndex(e => e.TestId)
                    .HasName("test_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("longtext")
                    .HasComment("Logs of current test")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.IsException)
                    .HasColumnName("is_exception")
                    .HasComment("Is current log test exception?");

                entity.Property(e => e.TestId)
                    .HasColumnName("test_id")
                    .HasComment("Test ID (test.id)");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("log_ibfk_1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.HasIndex(e => e.Name)
                    .HasName("project_name_u")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Project name (1000 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<RelFailReasonTest>(entity =>
            {
                entity.ToTable("rel_fail_reason_test");

                entity.HasIndex(e => e.FailReasonId)
                    .HasName("fail_reason_id");

                entity.HasIndex(e => e.TestId)
                    .HasName("test_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(10000)")
                    .HasComment("Fail reason comment (10000 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.FailReasonId)
                    .HasColumnName("fail_reason_id")
                    .HasComment("Fail reason ID (fail_reason.id)");

                entity.Property(e => e.TestId)
                    .HasColumnName("test_id")
                    .HasComment("Test ID (test.id)");

                entity.HasOne(d => d.FailReason)
                    .WithMany(p => p.RelFailReasonTest)
                    .HasForeignKey(d => d.FailReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_fail_reason_test_ibfk_1");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.RelFailReasonTest)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_fail_reason_test_ibfk_2");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuildNumber)
                    .HasColumnName("build_number")
                    .HasComment("Build number");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("Test start time");

                entity.Property(e => e.SessionKey)
                    .IsRequired()
                    .HasColumnName("session_key")
                    .HasColumnType("varchar(1000)")
                    .HasComment("Session key of current test running")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasComment("Status name (255 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("test");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("author_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("project_id");

                entity.HasIndex(e => e.SessionId)
                    .HasName("session_id");

                entity.HasIndex(e => e.StatusId)
                    .HasName("status_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasComment("Author info ID (author.id)");

                entity.Property(e => e.Browser)
                    .HasColumnName("browser")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("Browser used for tests execution (255 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("timestamp")
                    .HasComment("Test end time");

                entity.Property(e => e.Env)
                    .IsRequired()
                    .HasColumnName("env")
                    .HasColumnType("varchar(255)")
                    .HasComment("Node name where tests are executed (255 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasColumnName("method_name")
                    .HasColumnType("varchar(10000)")
                    .HasComment("Test method name (10000 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(10000)")
                    .HasComment("Test name (10000 symbols)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasComment("Project ID (project.id)");

                entity.Property(e => e.SessionId)
                    .HasColumnName("session_id")
                    .HasComment("ID of current test execution session (session.id)");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("Test start time");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasComment("Test execution status (status.id)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("test_ibfk_3");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("test_ibfk_1");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("test_ibfk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("test_ibfk_4");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("token");

                entity.HasIndex(e => e.Id)
                    .HasName("token_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Value)
                    .HasName("token_value_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.VariantId)
                    .HasName("token_variant_id_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8mb3_general_ci");

                entity.Property(e => e.VariantId).HasColumnName("variant_id");

                entity.HasOne(d => d.Variant)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.VariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("token_variant_id_fk");
            });

            modelBuilder.Entity<Variant>(entity =>
            {
                entity.ToTable("variant");

                entity.HasIndex(e => e.Id)
                    .HasName("variant_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GrammarMistakeOnSaveProject).HasColumnName("grammar_mistake_on_save_project");

                entity.Property(e => e.GrammarMistakeOnSaveTest).HasColumnName("grammar_mistake_on_save_test");

                entity.Property(e => e.IsDynamicVersionInFooter).HasColumnName("is_dynamic_version_in_footer");

                entity.Property(e => e.UseAjaxForTestsPage).HasColumnName("use_ajax_for_tests_page");

                entity.Property(e => e.UseAlertForNewProject).HasColumnName("use_alert_for_new_project");

                entity.Property(e => e.UseFrameForNewProject).HasColumnName("use_frame_for_new_project");

                entity.Property(e => e.UseGeolocationForNewProject).HasColumnName("use_geolocation_for_new_project");

                entity.Property(e => e.UseNewTabForNewProject).HasColumnName("use_new_tab_for_new_project");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
