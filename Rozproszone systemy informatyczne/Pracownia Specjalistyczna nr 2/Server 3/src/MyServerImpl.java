import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.Random;

public class MyServerImpl extends UnicastRemoteObject implements MyServerInt {
    public Character[][] game = new Character[3][];
    public ArrayList<Integer> numbers = new ArrayList<Integer>();
    public Random rand = new Random();

    protected MyServerImpl() throws RemoteException {
        super();
        game[0] = new Character[3];
        game[1] = new Character[3];
        game[2] = new Character[3];
        numbers.add(1);
        numbers.add(2);
        numbers.add(3);
        numbers.add(4);
        numbers.add(5);
        numbers.add(6);
        numbers.add(7);
        numbers.add(8);
        numbers.add(9);
    }

    public String Play(int position) throws RemoteException {
        switch (position) {
            case 1:
                game[0][0] = 'x';
                break;
            case 2:
                game[0][1] = 'x';
                break;
            case 3:
                game[0][2] = 'x';
                break;
            case 4:
                game[1][0] = 'x';
                break;
            case 5:
                game[1][1] = 'x';
                break;
            case 6:
                game[1][2] = 'x';
                break;
            case 7:
                game[2][0] = 'x';
                break;
            case 8:
                game[2][1] = 'x';
                break;
            case 9:
                game[2][2] = 'x';
                break;
            default:
                break;
        }

        if(CheckWin()) return "Wygrał Gracz";

        numbers.remove(position - 1);

        int n = rand.nextInt(numbers.size()) + 1;

        switch (n) {
            case 1:
                game[0][0] = 'o';
                break;
            case 2:
                game[0][1] = 'o';
                break;
            case 3:
                game[0][2] = 'o';
                break;
            case 4:
                game[1][0] = 'o';
                break;
            case 5:
                game[1][1] = 'o';
                break;
            case 6:
                game[1][2] = 'o';
                break;
            case 7:
                game[2][0] = 'o';
                break;
            case 8:
                game[2][1] = 'o';
                break;
            case 9:
                game[2][2] = 'o';
                break;
            default:
                break;
        }

        numbers.remove(n - 1);

        if(CheckWin()) return "Wygrał Komputer";

        return game[0][0] + " " + game[0][1] + " " + game[0][2] + " " + "\n" + game[1][0] + " " + game[1][1] + " "
                + game[1][2] + " " + "\n" + game[2][0] + " " + game[2][1] + " " + game[2][2] + " " + "\n";
    }

    public Boolean CheckWin(){
        if(game[0][0] != null && game[0][0].equals(game[0][1]) && game[0][2] != null && game[0][2].equals(game[0][0])) return true;
        if(game[1][0] != null && game[1][0].equals(game[1][1]) && game[1][2] != null && game[1][2].equals(game[1][0])) return true;
        if(game[2][0] != null && game[2][0].equals(game[2][1]) && game[2][2] != null && game[2][2].equals(game[2][0])) return true;
        if(game[0][0] != null && game[0][0].equals(game[1][0]) && game[2][0] != null && game[2][0].equals(game[0][0])) return true;
        if(game[0][1] != null && game[0][1].equals(game[1][1]) && game[2][1] != null && game[2][1].equals(game[0][1])) return true;
        if(game[0][2] != null && game[0][2].equals(game[1][2]) && game[2][2] != null && game[2][2].equals(game[0][2])) return true;
        if(game[0][0] != null && game[0][0].equals(game[0][1]) && game[2][2] != null && game[2][2].equals(game[0][0])) return true;
        if(game[0][2] != null && game[0][2].equals(game[1][1]) && game[2][0] != null && game[2][0].equals(game[0][2])) return true;
        return false;
    }
};
