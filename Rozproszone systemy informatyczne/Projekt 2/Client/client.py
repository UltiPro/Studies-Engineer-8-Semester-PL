import requests
import urllib3

urllib3.disable_warnings(
    urllib3.exceptions.InsecureRequestWarning
)  # wyłączenie warningów mordzia
# a pytasz dlaczego som?
#  otóż certyfikat jest podpisany przez samego siebie a ta biblioteka pythona jest na to wyczulona więc wjeb sobie to gdzię na początku

base_url = "https://localhost:7075/api/Motor"
cert_path = "./cert.crt"
cert_key = "./cert.key"

# PAMIĘTAJ O TYM
# dotnet dev-certs https -ep ./cert.crt -np --trust --format Pem

# dobrze jest mieć kod w try catchu ale to jak se tam chcesz

"""response = requests.get(
    base_url + "/all", cert=(cert_path, cert_key), verify=False
)  # zwykły get po path

if response.status_code >= 200 and response.status_code >= 300:
    print(response.json())
else:
    print(f"Zapytanie nie powiodło się. Kod statusu: {response.status_code}")
    print(response.text)

data = {
    "brand": "cos",
    "name": "cos",
    "requiredLicence": 2,
    "description": "cos",
    "rentPrice": 1515,
}

response = requests.post(
    base_url, cert=(cert_path, cert_key), verify=False, json=data
)  # POST przykład body

print("\n\n")

params_2 = {"id": 4}  # ofc możesz to wjebać bezpośrednio do zapytania

response = requests.get(
    base_url, cert=(cert_path, cert_key), verify=False, params=params_2
)  # zwykły get po query

print(response.json())"""

try:
    response = requests.get(
        base_url + "/pdf/4", cert=(cert_path, cert_key), verify=False
    )

    # Sprawdzenie, czy zapytanie zakończyło się sukcesem
    response.raise_for_status()

    # Zapisanie otrzymanego pliku PDF
    with open("./file.pdf", "wb") as file:
        file.write(response.content)

except Exception as e:
    print(e)
