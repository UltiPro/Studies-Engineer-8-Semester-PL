public class Chatter {

    public String name;
    public ChatI client;

    public Chatter(String name, ChatI client) {
        this.name = name;
        this.client = client;
    }

    public String getName() {
        return name;
    }

    public ChatI getClient() {
        return client;
    }
}
