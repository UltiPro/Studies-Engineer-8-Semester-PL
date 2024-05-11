using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Projekt_1_Web_Serwisy.Models;

public class DBMotorConfiguration : IEntityTypeConfiguration<DBMotor>
{
    public void Configure(EntityTypeBuilder<DBMotor> builder)
    {
        builder.HasData(new DBMotor
        {
            Id = 1,
            Brand = "Honda",
            Name = "PCX 125",
            RequiredLicence = Licence.B_A1,
            Description = "Koniec z czekaniem na drogie taksówki czy spóźniające się środki publicznego transportu — mając PCX125, możesz po prostu nacisnąć przycisk i ruszać. Smukła, dynamiczna sylwetka z łatwością pokonuje miejskie korki, dowożąc kierowcę do celu podróży w świetnym stylu — i na czas. ",
            RentPrice = 130,
            RentTo = null
        },
        new DBMotor
        {
            Id = 2,
            Brand = "KTM",
            Name = "ADVENTURE 390 SW",
            RequiredLicence = Licence.A2,
            Description = "Czasy, gdy przejeżdżało się z punktu A do punktu B należą już do przeszłości. Model KTM 390 ADVENTURE wyznacza nowe zasady codziennych dojazdów. Wszechstronność umożliwiająca jazdę po każdej nawierzchni, niezawodność i użyteczność na co dzień, a do tego gotowe na każdą przygodę koła szprychowe, sprawdzona jednostka napędowa o pojemności 373 cm³ i wiodące w swej klasie podwozie — KTM 390 ADVENTURE spełnia oczekiwania nie tylko poszukiwaczy przygód, ale i amatorów jazdy po bezdrożach.",
            RentPrice = 190,
            RentTo = null
        }, new DBMotor
        {
            Id = 3,
            Brand = "KTM",
            Name = "Duke 390",
            RequiredLicence = Licence.A2,
            Description = "KTM 390 DUKE to uosobienie cech, które decydują o tak licznym gronie amatorów szybkiej jazdy motocyklem w mieście. Ten mistrz zakrętów łączy w sobie maksymalną radość z jazdy z optymalną wartością użytkową i nie ma sobie równych, gdy liczy się zwrotność. Jest lekki jak piórko, mocny i wyposażony w najnowocześniejsze technologie — gwarantuje więc ekscytujące przeżycia niezależnie od tego, czy torujesz sobie drogę w miejskiej dżungli, czy dajesz o sobie znać w gąszczu zakrętów.",
            RentPrice = 170,
            RentTo = null
        }, new DBMotor
        {
            Id = 4,
            Brand = "KTM",
            Name = "790 ADVENTURE",
            RequiredLicence = Licence.A,
            Description = "Model KTM 790 ADVENTURE na rok 2024 zaprojektowano po to, aby ułatwić dalekie wyprawy na dwóch kołach. Dzielność w terenie, komfort jazdy, oszczędność: w przypadku tego modelu można po kolei odhaczyć wszystkie zalety, które powinien mieć motocykl wyprawowy, ale i tak wymyka się on wszelkim tego rodzaju charakterystykom. Zbudowany na bazie mocnego, dwucylindrowego silnika rzędowego i wyposażony w najlepszy w swojej klasie pakiet systemów elektronicznych, jest doskonałym wyborem dla wszystkich zamierzających sprawdzić, co jest za horyzontem.",
            RentPrice = 240,
            RentTo = null
        }, new DBMotor
        {
            Id = 5,
            Brand = "KTM",
            Name = "1290 SUPER ADVENTURE R",
            RequiredLicence = Licence.A,
            Description = "Wybierz punkt na mapie, ustaw pinezkę i ruszaj w drogę. Model KTM 1290 SUPER ADVENTURE R zaprojektowano do pokonywania najdzikszego, najtrudniejszego terenu z niezrównaną zwinnością i najlepszymi w tej klasie osiągami. Pozostaje nam tylko powiedzieć — uwaga na niezbadane tereny! KTM 1290 SUPER ADVENTURE R jest gotowy do odkrywania miejsc jeszcze nietkniętych ludzką stopą.",
            RentPrice = 290,
            RentTo = null
        },new DBMotor
        {
            Id = 6,
            Brand = "Honda",
            Name = "CB 650 R",
            RequiredLicence = Licence.A,
            Description = "To właśnie tutaj znajdziesz czystą magię czterech cylindrów. Motocykl został odchudzony, aby zapewnić niezwykłe osiągi - od miejskich przecznic po kręte boczne drogi oferuje mocne przyspieszenie, wyjątkową zwinność i wyrazistą stylistykę Neo Sports Café inspirowaną modelem CB1000R. Dodajmy do tego najwyższej klasy technologię oraz możliwość jazdy na pierwszym motocyklu z niesamowitym, nowym systemem sprzęgła E-Clutch Honda... czasami mniej naprawdę może znaczyć więcej.",
            RentPrice = 190,
            RentTo = null
        }, new DBMotor
        {
            Id = 7,
            Brand = "Honda",
            Name = "X-ADV",
            RequiredLicence = Licence.A2,
            Description = "X-ADV wytycza szlak. Łącząc możliwości i osiągi motocykla klasy adventure z komfortem, wygodą i zdolnościami do codziennej jazdy w mieście typowymi dla maxiskutera, jest niczym dwukołowy SUV wymykający się konwencji.",
            RentPrice = 190,
            RentTo = null
        },new DBMotor
        {
            Id = 8,
            Brand = "Honda",
            Name = "XL750 Transalp",
            RequiredLicence = Licence.A,
            Description = "Wiodąca w klasie moc i moment obrotowy oraz podwozie, które zapewnia lekkie prowadzenie na drodze i pełną kontrolę w terenie. Owiewka i szyba zbudowane z myślą o aerodynamice i ochronie przed wiatrem oraz optymalna pozycja kierowcy dla całodziennego komfortu jazdy. Nowa Honda XL750 Transalp zadaje tylko jedno pytanie. Jak daleko chcesz dojechać?",
            RentPrice = 210,
            RentTo = null
        }, new DBMotor
        {
            Id = 9,
            Brand = "Honda",
            Name = "CRF1100L Africa Twin",
            RequiredLicence = Licence.A,
            Description = "Africa Twin została właśnie ulepszona! Wciąż jest tym samym zwinnym motocyklem Adventure, jakim zawsze była — z osiągami pozwalającymi na przekraczanie granic. Teraz jednak ma wyższy moment obrotowy przy niskich prędkościach. Nowa regulowana szyba poprawia komfort jazdy. Opony bezdętkowe można łatwo naprawić. A opcja elektronicznego zawieszenie Africa Twin ES z Showa-EERA™ daje poszukiwaczom przygód zupełnie nowe możliwości.",
            RentPrice = 280,
            RentTo = null
        },new DBMotor
        {
            Id = 10,
            Brand = "Yamaha",
            Name = "NMAX 125",
            RequiredLicence = Licence.B_A1,
            Description = "Żyjemy w czasach, w których na nowo musimy zdefiniować środki komunikacji w mieście, a Yamaha NMAX 125 idealnie wpisuje się w wizję nowej mobilnościę Ten łatwy w obsłudze skuter miejski napędzany dynamicznym i ekonomicznym silnikiem o pojemności 125 cm3 pozwala na pokonanie około 300 km bez tankowania, jest wyposażony w najwyższej jakości wyposażenie i zapewnia wysoką specyfikację.",
            RentPrice = 130,
            RentTo = null
        }, new DBMotor
        {
            Id = 11,
            Brand = "Yamaha",
            Name = "TENERE 700",
            RequiredLicence = Licence.A,
            Description = "Za kierownicą Tenere 700 Twoja przyszłość będzie taka jak chcesz. Ponieważ ten wszechstronny motocykl sprawi, że ograniczenia znikną z Twojego życia, co pozwoli Ci doświadczyć uczucia totalnej wolności.",
            RentPrice = 210,
            RentTo = null
        },new DBMotor
        {
            Id = 12,
            Brand = "Yamaha",
            Name = "R3",
            RequiredLicence = Licence.A2,
            Description = "Świat R wzywa. A gdy zobaczysz, jak wiele możliwości oferuje model R3, przekonasz się, że to motocykl dla Ciebie. Wysokoobrotowy silnik o pojemności 321 cm3 zapewnia wyjątkowe przyspieszenia, a jego wiodąca w klasie jakość wykonania, nowa generacja kolorów Icon Blue i agresywna stylistyka potwierdzają, że R3 to najlepszy motocykl w klasie lekkich sportów.",
            RentPrice = 170,
            RentTo = null
        }, new DBMotor
        {
            Id = 13,
            Brand = "Yamaha",
            Name = "XSR125 LEGACY",
            RequiredLicence = Licence.B_A1,
            Description = "Ten lekki model z rodziny „Faster Sons” jest idealnym motocyklem dla wszystkich potrafiących docenić ponadczasowe wzornictwo i autentyczny styl, ale równocześnie oczekujących sportowych osiągów, jakie dają nowoczesne technologie silnika i zawieszenia. Zainspirowana przeszłością i stworzona z myślą o przyszłości, XSR125 Legacy to najlepszy wybór dla każdego motocyklisty - początkującego, doświadczonego i wracającego do jazdy po latach przerwy.",
            RentPrice = 140,
            RentTo = null
        },new DBMotor
        {
            Id = 14,
            Brand = "BMW",
            Name = "G 310 GS",
            RequiredLicence = Licence.A2,
            Description = "G 310 GS to idealny towarzysz do odkrywania miasta ale też przekraczania jego granic i podążania poza asfaltowe trasy. Dzięki wygodnej ergonomii GS i wyposażeniu wysokiej jakości, ten motocykl jest idealny do codziennej, zrelaksowanej jazdy w dowolne miejsce.",
            RentPrice = 160,
            RentTo = null
        }, new DBMotor
        {
            Id = 15,
            Brand = "BMW",
            Name = "F 900 GS",
            RequiredLicence = Licence.A2,
            Description = "F 900 GS został zoptymalizowany do jazdy w terenie przez zastosowanie wyższej kierownicy i niższych podnóżków. Dodatkowo dźwignia hamulca nożnego jest umieszczona wyżej i dzięki temu łatwiej ją dosięgnąć, również ze względu na niższe oparcie. Składany stopień zapewnia optymalną dostępność. Regulowane dźwignie zmiany biegów pozwalają znaleźć idealną pozycję niezależnie od tego, czy jeździsz w butach szosowych czy terenowych oraz niezależnie od tego, czy jeździsz na stojąco czy na siedząco. Wyjście z własnej strefy komfortu z F 900 GS jest przyjemnym przeżyciem.",
            RentPrice = 230,
            RentTo = null
        }
        );
    }
}
