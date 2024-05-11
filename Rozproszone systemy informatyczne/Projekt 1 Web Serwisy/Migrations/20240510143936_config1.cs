using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_1_Web_Serwisy.Migrations
{
    /// <inheritdoc />
    public partial class config1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Motors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Motors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequiredLicence",
                table: "Motors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Description", "Name", "RentPrice", "RequiredLicence" },
                values: new object[] { "Honda", "Koniec z czekaniem na drogie taksówki czy spóźniające się środki publicznego transportu — mając PCX125, możesz po prostu nacisnąć przycisk i ruszać. Smukła, dynamiczna sylwetka z łatwością pokonuje miejskie korki, dowożąc kierowcę do celu podróży w świetnym stylu — i na czas. ", "PCX 125", 130, 0 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "Description", "Name", "RentPrice", "RequiredLicence" },
                values: new object[] { "KTM", "Czasy, gdy przejeżdżało się z punktu A do punktu B należą już do przeszłości. Model KTM 390 ADVENTURE wyznacza nowe zasady codziennych dojazdów. Wszechstronność umożliwiająca jazdę po każdej nawierzchni, niezawodność i użyteczność na co dzień, a do tego gotowe na każdą przygodę koła szprychowe, sprawdzona jednostka napędowa o pojemności 373 cm³ i wiodące w swej klasie podwozie — KTM 390 ADVENTURE spełnia oczekiwania nie tylko poszukiwaczy przygód, ale i amatorów jazdy po bezdrożach.", "ADVENTURE 390 SW", 190, 1 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "Description", "Name", "RentPrice", "RequiredLicence" },
                values: new object[] { "KTM", "KTM 390 DUKE to uosobienie cech, które decydują o tak licznym gronie amatorów szybkiej jazdy motocyklem w mieście. Ten mistrz zakrętów łączy w sobie maksymalną radość z jazdy z optymalną wartością użytkową i nie ma sobie równych, gdy liczy się zwrotność. Jest lekki jak piórko, mocny i wyposażony w najnowocześniejsze technologie — gwarantuje więc ekscytujące przeżycia niezależnie od tego, czy torujesz sobie drogę w miejskiej dżungli, czy dajesz o sobie znać w gąszczu zakrętów.", "Duke 390", 170, 1 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "Description", "Name", "RentPrice", "RequiredLicence" },
                values: new object[] { "KTM", "Model KTM 790 ADVENTURE na rok 2024 zaprojektowano po to, aby ułatwić dalekie wyprawy na dwóch kołach. Dzielność w terenie, komfort jazdy, oszczędność: w przypadku tego modelu można po kolei odhaczyć wszystkie zalety, które powinien mieć motocykl wyprawowy, ale i tak wymyka się on wszelkim tego rodzaju charakterystykom. Zbudowany na bazie mocnego, dwucylindrowego silnika rzędowego i wyposażony w najlepszy w swojej klasie pakiet systemów elektronicznych, jest doskonałym wyborem dla wszystkich zamierzających sprawdzić, co jest za horyzontem.", "790 ADVENTURE", 240, 2 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Brand", "Description", "Name", "RentPrice", "RequiredLicence" },
                values: new object[] { "KTM", "Wybierz punkt na mapie, ustaw pinezkę i ruszaj w drogę. Model KTM 1290 SUPER ADVENTURE R zaprojektowano do pokonywania najdzikszego, najtrudniejszego terenu z niezrównaną zwinnością i najlepszymi w tej klasie osiągami. Pozostaje nam tylko powiedzieć — uwaga na niezbadane tereny! KTM 1290 SUPER ADVENTURE R jest gotowy do odkrywania miejsc jeszcze nietkniętych ludzką stopą.", "1290 SUPER ADVENTURE R", 290, 2 });

            migrationBuilder.InsertData(
                table: "Motors",
                columns: new[] { "Id", "Brand", "Description", "Name", "RentPrice", "RentTo", "RequiredLicence", "Reservation" },
                values: new object[,]
                {
                    { 6, "Honda", "To właśnie tutaj znajdziesz czystą magię czterech cylindrów. Motocykl został odchudzony, aby zapewnić niezwykłe osiągi - od miejskich przecznic po kręte boczne drogi oferuje mocne przyspieszenie, wyjątkową zwinność i wyrazistą stylistykę Neo Sports Café inspirowaną modelem CB1000R. Dodajmy do tego najwyższej klasy technologię oraz możliwość jazdy na pierwszym motocyklu z niesamowitym, nowym systemem sprzęgła E-Clutch Honda... czasami mniej naprawdę może znaczyć więcej.", "CB 650 R", 190, null, 2, false },
                    { 7, "Honda", "X-ADV wytycza szlak. Łącząc możliwości i osiągi motocykla klasy adventure z komfortem, wygodą i zdolnościami do codziennej jazdy w mieście typowymi dla maxiskutera, jest niczym dwukołowy SUV wymykający się konwencji.", "X-ADV", 190, null, 1, false },
                    { 8, "Honda", "Wiodąca w klasie moc i moment obrotowy oraz podwozie, które zapewnia lekkie prowadzenie na drodze i pełną kontrolę w terenie. Owiewka i szyba zbudowane z myślą o aerodynamice i ochronie przed wiatrem oraz optymalna pozycja kierowcy dla całodziennego komfortu jazdy. Nowa Honda XL750 Transalp zadaje tylko jedno pytanie. Jak daleko chcesz dojechać?", "XL750 Transalp", 210, null, 2, false },
                    { 9, "Honda", "Africa Twin została właśnie ulepszona! Wciąż jest tym samym zwinnym motocyklem Adventure, jakim zawsze była — z osiągami pozwalającymi na przekraczanie granic. Teraz jednak ma wyższy moment obrotowy przy niskich prędkościach. Nowa regulowana szyba poprawia komfort jazdy. Opony bezdętkowe można łatwo naprawić. A opcja elektronicznego zawieszenie Africa Twin ES z Showa-EERA™ daje poszukiwaczom przygód zupełnie nowe możliwości.", "CRF1100L Africa Twin", 280, null, 2, false },
                    { 10, "Yamaha", "Żyjemy w czasach, w których na nowo musimy zdefiniować środki komunikacji w mieście, a Yamaha NMAX 125 idealnie wpisuje się w wizję nowej mobilnościę Ten łatwy w obsłudze skuter miejski napędzany dynamicznym i ekonomicznym silnikiem o pojemności 125 cm3 pozwala na pokonanie około 300 km bez tankowania, jest wyposażony w najwyższej jakości wyposażenie i zapewnia wysoką specyfikację.", "NMAX 125", 130, null, 0, false },
                    { 11, "Yamaha", "Za kierownicą Tenere 700 Twoja przyszłość będzie taka jak chcesz. Ponieważ ten wszechstronny motocykl sprawi, że ograniczenia znikną z Twojego życia, co pozwoli Ci doświadczyć uczucia totalnej wolności.", "TENERE 700", 210, null, 2, false },
                    { 12, "Yamaha", "Świat R wzywa. A gdy zobaczysz, jak wiele możliwości oferuje model R3, przekonasz się, że to motocykl dla Ciebie. Wysokoobrotowy silnik o pojemności 321 cm3 zapewnia wyjątkowe przyspieszenia, a jego wiodąca w klasie jakość wykonania, nowa generacja kolorów Icon Blue i agresywna stylistyka potwierdzają, że R3 to najlepszy motocykl w klasie lekkich sportów.", "R3", 170, null, 1, false },
                    { 13, "Yamaha", "Ten lekki model z rodziny „Faster Sons” jest idealnym motocyklem dla wszystkich potrafiących docenić ponadczasowe wzornictwo i autentyczny styl, ale równocześnie oczekujących sportowych osiągów, jakie dają nowoczesne technologie silnika i zawieszenia. Zainspirowana przeszłością i stworzona z myślą o przyszłości, XSR125 Legacy to najlepszy wybór dla każdego motocyklisty - początkującego, doświadczonego i wracającego do jazdy po latach przerwy.", "XSR125 LEGACY", 140, null, 0, false },
                    { 14, "BMW", "G 310 GS to idealny towarzysz do odkrywania miasta ale też przekraczania jego granic i podążania poza asfaltowe trasy. Dzięki wygodnej ergonomii GS i wyposażeniu wysokiej jakości, ten motocykl jest idealny do codziennej, zrelaksowanej jazdy w dowolne miejsce.", "G 310 GS", 160, null, 1, false },
                    { 15, "BMW", "F 900 GS został zoptymalizowany do jazdy w terenie przez zastosowanie wyższej kierownicy i niższych podnóżków. Dodatkowo dźwignia hamulca nożnego jest umieszczona wyżej i dzięki temu łatwiej ją dosięgnąć, również ze względu na niższe oparcie. Składany stopień zapewnia optymalną dostępność. Regulowane dźwignie zmiany biegów pozwalają znaleźć idealną pozycję niezależnie od tego, czy jeździsz w butach szosowych czy terenowych oraz niezależnie od tego, czy jeździsz na stojąco czy na siedząco. Wyjście z własnej strefy komfortu z F 900 GS jest przyjemnym przeżyciem.", "F 900 GS", 230, null, 1, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Motors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Motors");

            migrationBuilder.DropColumn(
                name: "RequiredLicence",
                table: "Motors");

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "RentPrice" },
                values: new object[] { "a", 1000 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "RentPrice" },
                values: new object[] { "b", 1000 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "RentPrice" },
                values: new object[] { "c", 1000 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "RentPrice" },
                values: new object[] { "d", 1000 });

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "RentPrice" },
                values: new object[] { "e", 1000 });
        }
    }
}
