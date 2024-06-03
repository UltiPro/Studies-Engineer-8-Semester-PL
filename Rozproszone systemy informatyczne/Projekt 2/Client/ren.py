from datetime import datetime
import os
import tkinter as tk
from tkinter import messagebox
import requests
import subprocess
from requests.auth import HTTPBasicAuth

from add_popup import AddMotorcyclePopup
from edit_popup import EditMotorcyclePopup
from rent_popup import RentMotorcyclePopup

class MotorcycleRentalApp:
    def __init__(self, root, username, password):
        self.root = root
        self.root.title("Motorcycle Rental App")
        self.current_brand_filter = ""
        self.username = username  
        self.password = password
        self.create_gui()

        # certyfikat
        self.base_url = "https://localhost:7075/api/Motor"
        self.cert_path = "./cert.crt"
        self.cert_key = "./cert.key"

        self.display_motorcycles()

    def create_gui(self):
        filter_frame = tk.Frame(self.root)
        filter_frame.grid(row=0, column=0, sticky="ew", padx=10, pady=5)
        
        tk.Label(filter_frame, text="Filtruj po marce:").grid(row=0, column=1, sticky="w", padx=5, pady=5)
        self.brand_filter_entry = tk.Entry(filter_frame)
        self.brand_filter_entry.grid(row=0, column=2, sticky="ew", padx=5, pady=5) 
        tk.Button(filter_frame, text="Filtruj", command=self.filter_motorcycles).grid(row=0, column=3, sticky="e", padx=5, pady=5)

        tk.Button(filter_frame, text="Dodaj motocykl", command=self.open_add_motorcycle_popup).grid(row=0, column=0, sticky="e", padx=(10, 250), pady=5)


        table_frame = tk.Frame(self.root)
        table_frame.grid(row=1, column=0, sticky="nsew", padx=10, pady=(0, 10))

        self.table_frame = tk.Frame(table_frame)
        self.table_frame.pack(fill="both", expand=True)
        
    def open_add_motorcycle_popup(self):
        popup = AddMotorcyclePopup(self.root, self.base_url, self.cert_path, self.cert_key, self.username, self.password, self.display_motorcycles)

    def display_motorcycles(self):
        try:
            response = requests.get(
                self.base_url + "/all",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            motorcycles = response.json()

            for widget in self.table_frame.winfo_children():
                widget.destroy()

            tk.Label(self.table_frame, text="id", font=('Arial', 10, 'bold')).grid(row=0, column=0, padx=5, pady=5)
            tk.Label(self.table_frame, text="Marka", font=('Arial', 10, 'bold')).grid(row=0, column=1, padx=5, pady=5)
            tk.Label(self.table_frame, text="Nazwa", font=('Arial', 10, 'bold')).grid(row=0, column=2, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wymagane prawo jazdy", font=('Arial', 10, 'bold')).grid(row=0, column=3, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wynajęty do",font=('Arial', 10, 'bold')).grid(row=0, column=4, padx=5, pady=5)
            tk.Label(self.table_frame, text="Rezerwacja", font=('Arial', 10, 'bold')).grid(row=0, column=5, padx=5, pady=5) 
            tk.Label(self.table_frame, text="Cena za dobę", font=('Arial', 10, 'bold')).grid(row=0, column=6, padx=5, pady=5)
            tk.Label(self.table_frame, text="Opcje", font=('Arial', 10, 'bold')).grid(row=0, column=7, padx=5, pady=5)

            # Dodanie danych do tabeli
            for i, motor in enumerate(motorcycles):
                if self.current_brand_filter and self.current_brand_filter != motor["brand"]:
                    continue
                tk.Label(self.table_frame, text=motor["id"]).grid(row=i+1, column=0, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["brand"]).grid(row=i+1, column=1, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["name"]).grid(row=i+1, column=2, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["requiredLicence"]).grid(row=i+1, column=3, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["rentTo"]).grid(row=i+1, column=4, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["reservation"]).grid(row=i+1, column=5, padx=5, pady=5)
                tk.Label(self.table_frame, text=str(motor["rentPrice"]) + " zł").grid(row=i+1, column=6, padx=5, pady=5)
                options_frame = tk.Frame(self.table_frame)
                options_frame.grid(row=i+1, column=7, padx=5, pady=5)
                tk.Button(options_frame, text="Usuń", command=lambda id=motor["id"]: self.remove_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Edytuj", command=lambda id=motor["id"]: self.edit_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Szczegóły", command=lambda id=motor["id"]: self.show_motorcycle_details(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Rezerwuj", command=lambda id=motor["id"]: self.reserve_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Anuluj rezerwację", command=lambda id=motor["id"]: self.cancel_reservation(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Wypożycz", command=lambda id=motor["id"]: self.rent_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="PDF", command=lambda id=motor["id"]: self.generate_pdf(id)).pack(side=tk.LEFT, padx=2)
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def filter_motorcycles(self):
        self.current_brand_filter = self.brand_filter_entry.get()
        try:
            if self.current_brand_filter == '':
                self.display_motorcycles()
            
            else:
                response = requests.get(
                    f"{self.base_url}/find/{self.current_brand_filter}",
                    cert=(self.cert_path, self.cert_key),
                    verify=False
                )
                response.raise_for_status()
                motorcycles = response.json()


                for widget in self.table_frame.winfo_children():
                    widget.destroy()

                tk.Label(self.table_frame, text="id", font=('Arial', 10, 'bold')).grid(row=0, column=0, padx=5, pady=5)
                tk.Label(self.table_frame, text="Marka", font=('Arial', 10, 'bold')).grid(row=0, column=1, padx=5, pady=5)
                tk.Label(self.table_frame, text="Nazwa", font=('Arial', 10, 'bold')).grid(row=0, column=2, padx=5, pady=5)
                tk.Label(self.table_frame, text="Wymagane prawo jazdy", font=('Arial', 10, 'bold')).grid(row=0, column=3, padx=5, pady=5)
                tk.Label(self.table_frame, text="Wynajęty do",font=('Arial', 10, 'bold')).grid(row=0, column=4, padx=5, pady=5)
                tk.Label(self.table_frame, text="Rezerwacja", font=('Arial', 10, 'bold')).grid(row=0, column=5, padx=5, pady=5) 
                tk.Label(self.table_frame, text="Cena za dobę", font=('Arial', 10, 'bold')).grid(row=0, column=6, padx=5, pady=5)
                tk.Label(self.table_frame, text="Opcje", font=('Arial', 10, 'bold')).grid(row=0, column=7, padx=5, pady=5)

                # Dodaj nowe wpisy
                for i, motor in enumerate(motorcycles):
                    tk.Label(self.table_frame, text=motor["id"]).grid(row=i+1, column=0, padx=5, pady=5)
                    tk.Label(self.table_frame, text=motor["brand"]).grid(row=i+1, column=1, padx=5, pady=5)
                    tk.Label(self.table_frame, text=motor["name"]).grid(row=i+1, column=2, padx=5, pady=5)
                    tk.Label(self.table_frame, text=motor["requiredLicence"]).grid(row=i+1, column=3, padx=5, pady=5)
                    tk.Label(self.table_frame, text=motor["rentTo"]).grid(row=i+1, column=4, padx=5, pady=5)
                    tk.Label(self.table_frame, text=motor["reservation"]).grid(row=i+1, column=5, padx=5, pady=5)
                    tk.Label(self.table_frame, text=str(motor["rentPrice"]) + " zł").grid(row=i+1, column=6, padx=5, pady=5)
                    options_frame = tk.Frame(self.table_frame)
                    options_frame.grid(row=i+1, column=7, padx=5, pady=5)
                    tk.Button(options_frame, text="Usuń", command=lambda id=motor["id"]: self.remove_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="Edytuj", command=lambda id=motor["id"]: self.edit_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="Szczegóły", command=lambda id=motor["id"]: self.show_motorcycle_details(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="Rezerwuj", command=lambda id=motor["id"]: self.reserve_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="Anuluj rezerwację", command=lambda id=motor["id"]: self.cancel_reservation(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="Wypożycz", command=lambda id=motor["id"]: self.rent_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                    tk.Button(options_frame, text="PDF", command=lambda id=motor["id"]: self.generate_pdf(id)).pack(side=tk.LEFT, padx=2)
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def remove_motorcycle(self, id):
        try:
            response = requests.delete(
                f"{self.base_url}?id={id}",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            
            messagebox.showinfo("Sukces", "Motocykl został usunięty pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def edit_motorcycle(self, id):
        popup = EditMotorcyclePopup(self.root, self.base_url, self.cert_path, self.cert_key, self.username, self.password, id, self.display_motorcycles)

    def show_motorcycle_details(self, id):
        try:
            response = requests.get(
                f"{self.base_url}?id={id}",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            details = response.json()


            details_window = tk.Toplevel(self.root)
            details_window.title("Szczegóły motocykla")

            label_names = {
                'id': 'id',
                'brand': 'Marka',
                'name': 'Nazwa',
                'requiredLicence': 'Wymagane prawo jazdy',
                'rentPrice': 'Cena za dobę',
                'rentTo': 'Wynajęty do',
                'reservation': 'Rezerwacja'
            }

            for i, (key, label) in enumerate(label_names.items()):
                tk.Label(details_window, text=label, font=('Arial', 10, 'bold')).grid(row=0, column=i, padx=5, pady=5)
                if key == 'rentPrice':
                    tk.Label(details_window, text=str(details[key]) + " zł").grid(row=1, column=i, padx=5, pady=5)
                else:
                    tk.Label(details_window, text=details[key]).grid(row=1, column=i, padx=5, pady=5)

            description_text = tk.Text(details_window, wrap=tk.WORD, width=80, height=10)
            description_text.insert(tk.END, details['description'])
            description_text.config(state=tk.DISABLED)  
            description_text.grid(row=2, columnspan=len(label_names), padx=5, pady=5)

        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def reserve_motorcycle(self, id): 
        try:
            response = requests.post(
                f"{self.base_url}/reserve/{id}",
                cert=(self.cert_path, self.cert_key),
                verify=False
            )
            response.raise_for_status()

            messagebox.showinfo("Sukces", "Motocykl został zarezerwowany pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def cancel_reservation(self, id):
        try:
            response = requests.post(
                f"{self.base_url}/cancelreserve/{id}",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()

            messagebox.showinfo("Sukces", "Rezerwacja motocykla została anulowana pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def rent_motorcycle(self, id):
        popup = RentMotorcyclePopup(self.root, self.base_url, self.cert_path, self.cert_key, self.username, self.password, id, self.display_motorcycles)

    def generate_pdf(self, id):
        try:
            response = requests.get(
                f"{self.base_url}/pdf/{id}",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            pdf_content = response.content

            current_datetime = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
            file_name = f"Motorcycle_{id}_{current_datetime}.pdf"
            folder_path = "./PDF"

            if not os.path.exists(folder_path):
                os.makedirs(folder_path)
            file_path = os.path.join(folder_path, file_name)

            with open(file_path, 'wb') as file:
                file.write(pdf_content)
            subprocess.Popen([os.path.abspath(file_path)], shell=True)
            messagebox.showinfo("Sukces", f"PDF został wygenerowany pomyślnie. Plik zapisano jako: {file_path}")
        except Exception as e:
            messagebox.showerror("Błąd", str(e))
        
if __name__ == "__main__":
    root = tk.Tk()
    app = MotorcycleRentalApp(root)
    root.mainloop()