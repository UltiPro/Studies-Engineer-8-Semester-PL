import com.lavasoft.GeoIPService;
import com.lavasoft.GeoIPServiceSoap;
public class App {

    public static void main(String[] args) {
        GeoIPService geoIPService = new GeoIPService();
        GeoIPServiceSoap geoIPServiceSoap  = geoIPService.getGeoIPServiceSoap();
        String location = geoIPServiceSoap.getIpLocation("216.58.208.206");
        String location2 = geoIPServiceSoap.getIpLocation20("212.77.98.9");
        String location3 = geoIPServiceSoap.getCountryISO2ByName("216.58.208.206");
        String location4 = geoIPServiceSoap.getCountryNameByISO2("216.58.208.206");
        String location5 = geoIPServiceSoap.getIpLocation20("216.58.208.206");
        System.out.println(location);
        System.out.println(location2);
        System.out.println(location3);
        System.out.println(location4);
        System.out.println(location5);
    }
}
