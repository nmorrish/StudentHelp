#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentHelpRazor.Entities;

#nullable disable

namespace StudentHelpRazor.Data
{
    public partial class StudenthelpContext : DbContext
    {
        public StudenthelpContext()
        {
        }

        public StudenthelpContext(DbContextOptions<StudenthelpContext> options) : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentAssignment> StudentAssignment { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Course");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.Name, "UC_CourseName")
                    .IsUnique();

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StudentAssignment>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.AssignmentId }).HasName("PK_StudentAssignment");

                entity.ToTable("Student_Assignment");

                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");

                entity.Property(e => e.DateSubmitted).HasColumnType("datetime");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.StudentAssignment)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentAssignment_Assignment");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAssignment)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentAssignment_Student");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK_StudentCourse");

                entity.ToTable("Student_Course");

                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.EnrolledDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sarvesh", LastName = "Razesinem", Email = "sarvesh@razes.inem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Âst", LastName = "Râlukennol", Email = "âst@râluk.ennol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Muz", LastName = "Ulthushelbel", Email = "muz@ulthush.elbel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Baros", LastName = "Óruknoram", Email = "baros@óruk.noram", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dozeb", LastName = "Nuglushzaneg", Email = "dozeb@nuglush.zaneg", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gulgun", LastName = "Sutonsobìr", Email = "gulgun@suton.sobìr", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nisgak", LastName = "Eggutmemad", Email = "nisgak@eggut.memad", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Enog", LastName = "Nolêthikud", Email = "enog@nolêth.ikud", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thîdas", LastName = "Thabumang", Email = "thîdas@thabum.ang", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ishol", LastName = "Astodkukon", Email = "ishol@astod.kukon", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shasar", LastName = "Rurastorstist", Email = "shasar@rurast.orstist", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Babin", LastName = "Lushûtograd", Email = "babin@lushût.ograd", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lumash", LastName = "Ilbådushul", Email = "lumash@ilbåd.ushul", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Eshim", LastName = "Domasstukos", Email = "eshim@domas.stukos", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rabed", LastName = "Duthnuredim", Email = "rabed@duthnur.edim", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tiklom", LastName = "Sithebnär", Email = "tiklom@sitheb.när", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shosêl", LastName = "Båltoltot", Email = "shosêl@bål.toltot", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ézneth", LastName = "Ûzvozbel", Email = "ézneth@ûz.vozbel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zefon", LastName = "Bolezuk", Email = "zefon@bol.ezuk", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Akam", LastName = "Munèstatem", Email = "akam@munèst.atem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Osram", LastName = "Tomêmstigaz", Email = "osram@tomêm.stigaz", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Geshud", LastName = "Uzarrazot", Email = "geshud@uzar.razot", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dumed", LastName = "Edtûlasàs", Email = "dumed@edtûl.asàs", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ìltang", LastName = "Enolzokgen", Email = "ìltang@enol.zokgen", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nalish", LastName = "Mithmisdîshmab", Email = "nalish@mithmis.dîshmab", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Fotthor", LastName = "Zágodumoz", Email = "fotthor@zágod.umoz", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Atul", LastName = "Sumundetes", Email = "atul@sumun.detes", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nog", LastName = "Nothisudar", Email = "nog@nothis.udar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Cabnul", LastName = "Omerron", Email = "cabnul@omer.ron", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ost", LastName = "Dalunib", Email = "ost@dal.unib", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Amug", LastName = "Saràmatêsh", Email = "amug@saràm.atêsh", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Atham", LastName = "Sizled", Email = "atham@siz.led", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gigin", LastName = "Belimsal", Email = "gigin@bel.imsal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Fenglel", LastName = "Asënarkim", Email = "fenglel@asën.arkim", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Bem", LastName = "Mondûlfullut", Email = "bem@mondûl.fullut", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Omet", LastName = "Isinlestus", Email = "omet@isin.lestus", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Totmon", LastName = "Gídthurnimak", Email = "totmon@gídthur.nimak", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dakost", LastName = "Mashuskàs", Email = "dakost@mashus.kàs", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Låluth", LastName = "Lolokesar", Email = "låluth@lolok.esar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tetist", LastName = "Masbukèt", Email = "tetist@mas.bukèt", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Vuthil", LastName = "Eribiseth", Email = "vuthil@erib.iseth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nëlas", LastName = "Dedrosesrel", Email = "nëlas@dedros.esrel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ar", LastName = "Kurigzukthist", Email = "ar@kurig.zukthist", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Astan", LastName = "Eshonùshrir", Email = "astan@eshon.ùshrir", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Eststek", LastName = "Igangluslem", Email = "eststek@igang.luslem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gonggash", LastName = "Limuluvir", Email = "gonggash@limul.uvir", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Engig", LastName = "Nòmmegob", Email = "engig@nòm.megob", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Asiz", LastName = "Belbezthetust", Email = "asiz@belbez.thetust", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thabost", LastName = "Katenam", Email = "thabost@kat.enam", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ned", LastName = "Desgirceshfot", Email = "ned@desgir.ceshfot", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Keshan", LastName = "Uburfongbez", Email = "keshan@ubur.fongbez", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Solon", LastName = "Ngobolnônub", Email = "solon@ngobol.nônub", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dur", LastName = "Rabelbost", Email = "dur@rab.elbost", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rerras", LastName = "Regfurgig", Email = "rerras@reg.furgig", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ngitkar", LastName = "Igestbesmar", Email = "ngitkar@igest.besmar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tatlosh", LastName = "Anilngathsesh", Email = "tatlosh@anil.ngathsesh", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ziril", LastName = "Vumsharkasith", Email = "ziril@vumshar.kasith", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ók", LastName = "Eserlashëd", Email = "ók@eser.lashëd", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Atöl", LastName = "Kåtdirarkoth", Email = "atöl@kåtdir.arkoth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Stagshil", LastName = "Bebmalgashcoz", Email = "stagshil@bebmal.gashcoz", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zikâth", LastName = "Àlilvildang", Email = "zikâth@àlil.vildang", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Vutok", LastName = "Almôshbatôk", Email = "vutok@almôsh.batôk", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Bardum", LastName = "Fikodräm", Email = "bardum@fikod.räm", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zikel", LastName = "Figulkethil", Email = "zikel@figul.kethil", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zotir", LastName = "Atzulsastres", Email = "zotir@atzul.sastres", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Fikuk", LastName = "Amkinkudust", Email = "fikuk@amkin.kudust", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dotir", LastName = "Usirkamuk", Email = "dotir@usir.kamuk", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Korsid", LastName = "Saruthnikuz", Email = "korsid@saruth.nikuz", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Roduk", LastName = "Gintarteshkad", Email = "roduk@gintar.teshkad", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nidòst", LastName = "Thobgemur", Email = "nidòst@thob.gemur", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Odkish", LastName = "Rúbaludiz", Email = "odkish@rúbal.udiz", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ustuth", LastName = "Egenmerir", Email = "ustuth@egen.merir", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Riril", LastName = "Koladnilgin", Email = "riril@kolad.nilgin", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Urus", LastName = "Ngotollibad", Email = "urus@ngotol.libad", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ked", LastName = "Ërtonglâven", Email = "ked@ërtong.lâven", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ódad", LastName = "Kastmubun", Email = "ódad@kast.mubun", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nist", LastName = "Gäremmidor", Email = "nist@gärem.midor", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ston", LastName = "Kadôltezad", Email = "ston@kadôl.tezad", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Urem", LastName = "Olonïteb", Email = "urem@olon.ïteb", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tarem", LastName = "Listniral", Email = "tarem@list.niral", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Er", LastName = "Inodmistêm", Email = "er@inod.mistêm", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ivom", LastName = "Kèbmakagsal", Email = "ivom@kèbmak.agsal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shis", LastName = "Goraldeleth", Email = "shis@goral.deleth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lemlor", LastName = "Insélïtsas", Email = "lemlor@insél.ïtsas", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sogdol", LastName = "Bakatinrus", Email = "sogdol@bakat.inrus", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shan", LastName = "Sïsaläkil", Email = "shan@sïsal.äkil", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Belal", LastName = "Degëldimshas", Email = "belal@degël.dimshas", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Seth", LastName = "Sholèbolnen", Email = "seth@sholèb.olnen", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Umid", LastName = "Dushigkomut", Email = "umid@dushig.komut", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tabar", LastName = "Vutramrèt", Email = "tabar@vutram.rèt", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Isul", LastName = "Zaludtathtat", Email = "isul@zalud.tathtat", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ungèg", LastName = "Thusestkagmel", Email = "ungèg@thusest.kagmel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Uzlir", LastName = "Sarveshîm", Email = "uzlir@sarvesh.îm", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lînem", LastName = "Âstudist", Email = "lînem@âst.udist", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ker", LastName = "Muztitthal", Email = "ker@muz.titthal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ruzos", LastName = "Barosstisträs", Email = "ruzos@baros.stisträs", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Onrel", LastName = "Dozebkugik", Email = "onrel@dozeb.kugik", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sombith", LastName = "Gulguntok", Email = "sombith@gulgun.tok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ramtak", LastName = "Nisgakerok", Email = "ramtak@nisgak.erok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zamnuth", LastName = "Enogshem", Email = "zamnuth@enog.shem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thasdoth", LastName = "Thîdasnitom", Email = "thasdoth@thîdas.nitom", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ngilok", LastName = "Isholsil", Email = "ngilok@ishol.sil", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Egståk", LastName = "Shasarison", Email = "egståk@shasar.ison", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gedor", LastName = "Babinmamot", Email = "gedor@babin.mamot", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Dèg", LastName = "Lumashzimkel", Email = "dèg@lumash.zimkel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Esäst", LastName = "Eshimelcur", Email = "esäst@eshim.elcur", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Râmol", LastName = "Rabeddamol", Email = "râmol@rabed.damol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ostath", LastName = "Tiklomgor", Email = "ostath@tiklom.gor", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ninur", LastName = "Shosêlal", Email = "ninur@shosêl.al", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Othör", LastName = "Éznethumril", Email = "othör@ézneth.umril", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Maskir", LastName = "Zefonrusest", Email = "maskir@zefon.rusest", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Okin", LastName = "Akamobok", Email = "okin@akam.obok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tinöth", LastName = "Osramthuveg", Email = "tinöth@osram.thuveg", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sharast", LastName = "Geshudtekkud", Email = "sharast@geshud.tekkud", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sefol", LastName = "Dumedsokan", Email = "sefol@dumed.sokan", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kemsor", LastName = "Ìltangugeth", Email = "kemsor@ìltang.ugeth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Arngish", LastName = "Nalishzimesh", Email = "arngish@nalish.zimesh", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Stistmig", LastName = "Fotthorrithul", Email = "stistmig@fotthor.rithul", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nakas", LastName = "Atulbab", Email = "nakas@atul.bab", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lushôn", LastName = "Nogfimshel", Email = "lushôn@nog.fimshel", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zethruk", LastName = "Cabnuleges", Email = "zethruk@cabnul.eges", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Minbaz", LastName = "Ostìrlom", Email = "minbaz@ost.ìrlom", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kisat", LastName = "Amugthethrus", Email = "kisat@amug.thethrus", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thalal", LastName = "Athamshithath", Email = "thalal@atham.shithath", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sedur", LastName = "Giginator", Email = "sedur@gigin.ator", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kurol", LastName = "Fenglelrumred", Email = "kurol@fenglel.rumred", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kastol", LastName = "Bemrodem", Email = "kastol@bem.rodem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Todör", LastName = "Ometthet", Email = "todör@omet.thet", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Okir", LastName = "Totmonib", Email = "okir@totmon.ib", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Mogshum", LastName = "Dakostkovest", Email = "mogshum@dakost.kovest", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Acöb", LastName = "Låluthmadush", Email = "acöb@låluth.madush", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rùkal", LastName = "Tetistilrom", Email = "rùkal@tetist.ilrom", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Èshgor", LastName = "Vuthilginok", Email = "èshgor@vuthil.ginok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sárek", LastName = "Nëlasbisól", Email = "sárek@nëlas.bisól", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Deb", LastName = "Arlîlar", Email = "deb@ar.lîlar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ber", LastName = "Astannimar", Email = "ber@astan.nimar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rir", LastName = "Eststekgemsit", Email = "rir@eststek.gemsit", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lorsïth", LastName = "Gonggashgeth", Email = "lorsïth@gonggash.geth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Atír", LastName = "Engigishen", Email = "atír@engig.ishen", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Siknug", LastName = "Asizcustith", Email = "siknug@asiz.custith", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kokeb", LastName = "Thabostétol", Email = "kokeb@thabost.étol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Etest", LastName = "Nedthimshur", Email = "etest@ned.thimshur", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Vesh", LastName = "Keshandugal", Email = "vesh@keshan.dugal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tath", LastName = "Solonmonom", Email = "tath@solon.monom", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Edir", LastName = "Durul", Email = "edir@dur.ul", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tig", LastName = "Rerrastetthush", Email = "tig@rerras.tetthush", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Astis", LastName = "Ngitkargatin", Email = "astis@ngitkar.gatin", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sakub", LastName = "Tatloshkulin", Email = "sakub@tatlosh.kulin", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Åmmeb", LastName = "Zirilkogsak", Email = "åmmeb@ziril.kogsak", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Talin", LastName = "Ókabal", Email = "talin@ók.abal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Óboth", LastName = "Atölkosoth", Email = "óboth@atöl.kosoth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shuk", LastName = "Stagshillogem", Email = "shuk@stagshil.logem", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shegum", LastName = "Zikâthzulash", Email = "shegum@zikâth.zulash", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sub", LastName = "Vutokmörul", Email = "sub@vutok.mörul", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Kosak", LastName = "Bardumiden", Email = "kosak@bardum.iden", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ór", LastName = "Zikeldáthnes", Email = "ór@zikel.dáthnes", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thosbut", LastName = "Zotirshigós", Email = "thosbut@zotir.shigós", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zag", LastName = "Fikukudril", Email = "zag@fikuk.udril", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Stidest", LastName = "Dotirmïkstal", Email = "stidest@dotir.mïkstal", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rëmrit", LastName = "Korsidsanreb", Email = "rëmrit@korsid.sanreb", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nåzom", LastName = "Rodukritas", Email = "nåzom@roduk.ritas", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Zêvut", LastName = "Nidòstzar", Email = "zêvut@nidòst.zar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ogon", LastName = "Odkishstegëth", Email = "ogon@odkish.stegëth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Fak", LastName = "Ustuthinash", Email = "fak@ustuth.inash", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Måmgoz", LastName = "Ririlshed", Email = "måmgoz@riril.shed", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gomóm", LastName = "Uruslotol", Email = "gomóm@urus.lotol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Sacat", LastName = "Kedvabôk", Email = "sacat@ked.vabôk", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tun", LastName = "Ódadmostib", Email = "tun@ódad.mostib", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Okbod", LastName = "Nistsom", Email = "okbod@nist.som", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nuggad", LastName = "Stonmuved", Email = "nuggad@ston.muved", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thun", LastName = "Uremmothdast", Email = "thun@urem.mothdast", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Akest", LastName = "Taremanön", Email = "akest@tarem.anön", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Rodnul", LastName = "Erlod", Email = "rodnul@er.lod", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Róth", LastName = "Ivomdakon", Email = "róth@ivom.dakon", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Idar", LastName = "Shislosush", Email = "idar@shis.losush", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Enas", LastName = "Lemloruzol", Email = "enas@lemlor.uzol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Birir", LastName = "Sogdolkebon", Email = "birir@sogdol.kebon", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Nomes", LastName = "Shanmigrur", Email = "nomes@shan.migrur", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shoduk", LastName = "Belaldolok", Email = "shoduk@belal.dolok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Minran", LastName = "Sethazmol", Email = "minran@seth.azmol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gingim", LastName = "Umidthokdeg", Email = "gingim@umid.thokdeg", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Enten", LastName = "Tabarnar", Email = "enten@tabar.nar", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ashzos", LastName = "Isulnothok", Email = "ashzos@isul.nothok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Tastrod", LastName = "Ungègusib", Email = "tastrod@ungèg.usib", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ruthösh", LastName = "Uzlirmebzuth", Email = "ruthösh@uzlir.mebzuth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Olum", LastName = "Lînemkin", Email = "olum@lînem.kin", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Assog", LastName = "Kerishlum", Email = "assog@ker.ishlum", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gan", LastName = "Ruzosnunok", Email = "gan@ruzos.nunok", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Shungmag", LastName = "Onrelkatthir", Email = "shungmag@onrel.katthir", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Vashzud", LastName = "Sombithlisat", Email = "vashzud@sombith.lisat", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gorroth", LastName = "Ramtakotam", Email = "gorroth@ramtak.otam", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Thazor", LastName = "Zamnuthtezul", Email = "thazor@zamnuth.tezul", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ozleb", LastName = "Thasdothzaled", Email = "ozleb@thasdoth.zaled", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Amur", LastName = "Ngilokrîthol", Email = "amur@ngilok.rîthol", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Gethor", LastName = "Egståkdisuth", Email = "gethor@egståk.disuth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ôtthat", LastName = "Gedoranan", Email = "ôtthat@gedor.anan", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Bekar", LastName = "Dègstosêth", Email = "bekar@dèg.stosêth", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Ùk", LastName = "Esästnecak", Email = "ùk@esäst.necak", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Storlut", LastName = "Râmolital", Email = "storlut@râmol.ital", RegistrationDate = DateTime.Now },
                new Student() { StudentId = Guid.NewGuid(), FirstName = "Lunrud", LastName = "Ostathrotik", Email = "lunrud@ostath.rotik", RegistrationDate = DateTime.Now });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}