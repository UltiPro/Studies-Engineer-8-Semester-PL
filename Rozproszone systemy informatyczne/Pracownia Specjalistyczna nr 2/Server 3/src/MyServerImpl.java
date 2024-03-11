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

        int row, col;

        switch (position) {
            case 1:
                row = 0; col = 0;
                break;
            case 2:
                row = 0; col = 1;
                break;
            case 3:
                row = 0; col = 2;
                break;
            case 4:
                row = 1; col = 0;
                break;
            case 5:
                row = 1; col = 1;
                break;
            case 6:
                row = 1; col = 2;
                break;
            case 7:
                row = 2; col = 0;
                break;
            case 8:
                row = 2; col = 1;
                break;
            case 9:
                row = 2; col = 2;
                break;
            default:
                return "Nieprawidłowa pozycja.";
        }

        if (game[row][col] != null) {
            return "Pole jest już zajęte, spróbuj ponownie.";
        }

        game[row][col] = 'x';

        if (CheckWin()) {
            return "Wygrał Gracz";
        }

        numbers.remove(position - 1);

        int n;
        do {
            n = rand.nextInt(numbers.size());
            position = numbers.get(n);
            row = (position - 1) / 3;
            col = (position - 1) % 3;
        } while (game[row][col] != null);
        
        game[row][col] = 'o';

        numbers.remove(n);

        if (CheckWin()) {
            return "Wygrał Komputer";
        }
        
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