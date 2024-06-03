from tkinter import simpledialog, messagebox, Tk, Frame, Label, Entry, Button
import requests
from ren import MotorcycleRentalApp  

class LoginWindow:
    def __init__(self, root):
        self.root = root
        self.root.title("Logowanie")

        self.username = None
        self.password = None

        self.create_gui()

    def create_gui(self):
        login_frame = Frame(self.root)
        login_frame.pack(padx=20, pady=10)

        Label(login_frame, text="Nazwa użytkownika:").grid(row=0, column=0, padx=5, pady=5)
        self.username_entry = Entry(login_frame)
        self.username_entry.grid(row=0, column=1, padx=5, pady=5)

        Label(login_frame, text="Hasło:").grid(row=1, column=0, padx=5, pady=5)
        self.password_entry = Entry(login_frame, show="*")
        self.password_entry.grid(row=1, column=1, padx=5, pady=5)

        Button(login_frame, text="Zaloguj", command=self.login).grid(row=2, columnspan=2, padx=5, pady=5)

    def login(self):
        self.username = self.username_entry.get()
        self.password = self.password_entry.get()

        try:
            self.root.destroy()  
            root = Tk() 
            app = MotorcycleRentalApp(root, self.username, self.password)
            root.mainloop()
        except requests.HTTPError as e:
            messagebox.showerror("Błąd logowania", "Nieprawidłowa nazwa użytkownika lub hasło.")

if __name__ == "__main__":
    root = Tk()
    login_window = LoginWindow(root)
    root.mainloop()
