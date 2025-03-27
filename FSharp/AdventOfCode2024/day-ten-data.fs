module DayTenData 

type directionType = UP | DOWN | LEFT | RIGHT
type matrixPosition = { x : int; y : int; v : char }
type matrixPositionCount = { position : matrixPosition; count : int }

let matrixTestShallow = [
    "9990999"
    "7771777"
    "8772756"
    "6543456"
    "7555777"
    "8555558"
    "9550559"
]
let matrixTestDeep = [
    "89010123"
    "78121874"
    "87430965"
    "96549874"
    "45678903"
    "32019012"
    "01329801"
    "10456732"
]

let matrixReal = [
    "65456780121098450187634569763232345103213014321223456"
    "56565097632787543296323478890141234234102125630310767"
    "67874198545601612345610165765650105145693496543407898"
    "32923238723432709654326543034789076008786587854343210"
    "41010129610549878723017832129878989019895674963210349"
    "03498034534698965014898901038965432123107895674310478"
    "12567012345787854198743230123453219654256456789321565"
    "01698107659834723085650134562104508723340349876459874"
    "98787238543225610176590121875698698812451210145960123"
    "07456549850110765203485430984788767904322109236871234"
    "12349654763234894312176534983109456765012218347120105"
    "45898763120125234523054325672112309876544367898034876"
    "36765812034576127654369010766043218987432458765565989"
    "29876903107681038923478001897652345898901089014877856"
    "10945789218996987610569123078901236754321198123966987"
    "89032654309345678510787430121230109865120567001855496"
    "70121023498210709425698945230381210778034056532743345"
    "63210110567601812334321876545496321689765123443412210"
    "54216787014503956745430965496587434509894329856301101"
    "09105496923212345866787634987676543213456018763456932"
    "18012387836781078975898523432345232122347890345367873"
    "27601078745896769784309410541230143021038901276216784"
    "34587669654305854693210305670321056523427987689105698"
    "03490548741214903543211234781987897012310094576543567"
    "12321239230903212650106521892016798987454123467612430"
    "12332102107812121786787430752105678901960013438900421"
    "01489237876543080695890124567234787017871012543321530"
    "90574323965012891234301233208943296323702387654487651"
    "87665010121026700343210120112350185410213498703599341"
    "96556901212345619656903234013461234554396569812678210"
    "03467832303434328743874105684570789689487876101432387"
    "12346345694540345012565106799688778776536964566501498"
    "01653210789691236522210239888799669890123453677890567"
    "18764101988780987121329845677234550187652122989108906"
    "89765001877101071030456776540112341290343001010267210"
    "67896982769612132549105689432101032301234578981354310"
    "78767853458743247678014988654312121000225665432458981"
    "69656765432658958976529812763243029810116767898567652"
    "30345899891067867789438701890156710723209852101438943"
    "21230014780890986290105610981260823654367643067324321"
    "10101423634761870121234327876501994569498543458015100"
    "45672344543052765436543234566782987678534412109176211"
    "34985495652143781287430110545093456767623303678989321"
    "43856784743430690398710325432112129865414510510076450"
    "32981012892121589459621234521001036772301623423165569"
    "01670143345023498764560149693456345681066739654234678"
    "12365294256510107643498238782347652397659848765985589"
    "03454385107898212532567345601098701498943707678876432"
    "12763476543789123431052101598709876510232110569845001"
    "29854307630076034521043015691612389323101023454032132"
    "38765218921165087654321126780543432134569878998108941"
    "47894356712234198765230105621234567023878565089237650"
    "21012349803343289890121234310123498012967432176546321"
]


