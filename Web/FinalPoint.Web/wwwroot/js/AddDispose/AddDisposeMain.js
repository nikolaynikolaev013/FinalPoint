    let isFragileCheckBox = document.querySelector("#IsFragile");
    let dontPaletizeCheckBox = document.querySelector("#DontPaletize");
    let cashOnDeliveryCheckBox = document.querySelector("#HasCashOnDelivery");
    let cashOnDeliveryPriceEl = document.querySelector("#cashOnDeliveryPriceBlock")
    let cashOnDeliveryPriceInput = document.querySelector("#CashOnDeliveryPrice");

    let widthEl = document.querySelector("#Width");
    let heightEl = document.querySelector("#Height");
    let lengthEl = document.querySelector("#Length");

    let weightEl = document.querySelector("#Weight");
    let numOfPartsEl = document.querySelector("#NumberOfParts");

    let deliveryPriceEl = document.querySelector("#deliveryPrice");
    let fullPriceEl = document.querySelector("#fullPrice");

        cashOnDeliveryCheckBox.addEventListener("click", () => {

            if (cashOnDeliveryCheckBox.checked) {
        cashOnDeliveryPriceEl.hidden = false;
            }
            else {
        cashOnDeliveryPriceEl.hidden = true;
                cashOnDeliveryPriceInput.textContent = 0.0;
            }

            calculatePrice();

        })


        function calculatePrice() {
        let widthValue = widthEl.value == 0 ? 1.0 : widthEl.value;
            let heightValue = heightEl.value == 0 ? 1.0 : heightEl.value;
            let lengthValue = lengthEl.value == 0 ? 1.0 : lengthEl.value;
            let weightValue = weightEl.Value == 0 ? 1.0 : weightEl.value;
            let numberOfParts = numOfPartsEl.value;

            let volumeWeight = (widthValue * heightValue * lengthValue);

            let cashOnDeliveryValue = cashOnDeliveryPriceInput.value;
            let finalPrice = 0;

            $.ajax({
        method: "GET",
                url: `/AddDispose/CalculateDeliveryPrice?height=${heightValue}&length=${lengthValue}&width=${widthValue}&weight=${weightValue}&hasCashOnDelivery=${cashOnDeliveryCheckBox.checked}&isFragile=${isFragileCheckBox.checked}&dontPaletize=${dontPaletizeCheckBox.checked}&cashOnDeliveryPrice=${cashOnDeliveryValue}&numOfParts=${numberOfParts}`,
                success: function (res) {
        finalPrice = res;
                    deliveryPriceEl.textContent = finalPrice.toFixed(2);
                    fullPriceEl.textContent = (finalPrice + Number(cashOnDeliveryValue)).toFixed(2);
                }
            });

            if (numberOfParts <= 0) {
        numOfPartsEl.value = 1;
            }
            if (weightEl.value < 0.1) {
        weightEl.value = 1;
            }
        }

        calculatePrice();

        function addNewClient(type, isEdit = false) {
        let currClient = document.querySelector("#currClient" + type.toString());
            let addClient = document.querySelector("#addClient" + type.toString());
            let addClientFields = document.querySelector("#addClient" + type.toString() + " .addClientFields");
            let addClientEditPanel = document.querySelector("#addClient" + type.toString() + " .addClientEditPanel");
            let errorField = addClientFields.querySelector(".errorField");

            if (!currClient.hidden) {
        currClient.hidden = true;
                addClientFields.hidden = false;
                addClient.hidden = false;

                if (isEdit) {
        let idField = addClientFields.querySelector(".id");
                    let firstNameField = addClientFields.querySelector(".firstName");
                    let lastNameField = addClientFields.querySelector(".lastName");
                    let phoneNumberField = addClientFields.querySelector(".phoneNumber");
                    let emailField = addClientFields.querySelector(".email");
                    let addressField = addClientFields.querySelector(".address");
                    let id = currClient.querySelector("select").value;

                    $.ajax({
        method: "GET",
                        url: `/AddDispose/GetClientDetailsById?id=` + id,
                        success: function (clientDetails) {
        firstNameField.value = clientDetails.firstName;
                            lastNameField.value = clientDetails.lastName;
                            phoneNumberField.value = clientDetails.phoneNumber;
                            emailField.value = clientDetails.email;
                            addressField.value = clientDetails.address;
                            idField.value = clientDetails.id
                        }
                    });
                    addClientEditPanel.hidden = false;
                }
            } else {
        currClient.hidden = false;
                addClientFields.hidden = true;
                addClient.hidden = true;
                addClientEditPanel.hidden = true;
                errorField.hidden = true;
            }


        }

        function saveClientData(type)
        {
        let addClientFields = document.querySelector("#addClient" + type.toString() + " .addClientFields");

            let idField = addClientFields.querySelector(".id");
            let firstNameField = addClientFields.querySelector(".firstName");
            let lastNameField = addClientFields.querySelector(".lastName");
            let phoneNumberField = addClientFields.querySelector(".phoneNumber");
            let emailField = addClientFields.querySelector(".email");
            let addressField = addClientFields.querySelector(".address");

            $.ajax({
        method: "POST",
                url: `/AddDispose/EditClientDetails`,
                data: {
        id: idField.value,
                    firstName: firstNameField.value,
                    lastName: lastNameField.value,
                    phoneNumber: phoneNumberField.value,
                    email: emailField.value,
                    address: addressField.value,
                },
                success: function (res) {

                    if (res.success) {
        let currClient = document.querySelector("#currClient" + type.toString());
                        let selectedItem = currClient.querySelector(".filter-option-inner-inner");
                        let selectItemsInDropdown = document.querySelectorAll('option[value="' + idField.value + '"]');

                        let newSelectItemValue = `${firstNameField.value} ${lastNameField.value} - ${phoneNumberField.value}`;

                        for (const dropdownItem of selectItemsInDropdown) {
        dropdownItem.textContent = newSelectItemValue;
                        }

                        selectedItem.textContent = newSelectItemValue;

                        addNewClient(type);
                    } else {
        let errorField = addClientFields.querySelector(".errorField");
                        errorField.hidden = false;
                        errorField.textContent  = res.message;

                    }
                }
            });
        }
