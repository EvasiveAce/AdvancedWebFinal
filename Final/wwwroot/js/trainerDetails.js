"using strict";

const baseAddress = "https://localhost:7032";

// _trainerModal is a self using function that basically grabs the required
// id's as well as sets the buttons to the click event.

(function _trainerModal() {
    _clearErrorMessages();
    const createTrainerModalDom = document.querySelector("#createTrainerModal");
    const createTrainerModal = new bootstrap.Modal(createTrainerModalDom);
    const createTrainerButton = document.querySelector("#btnCreateTrainer");
    createTrainerButton.addEventListener("click", event => {
        createTrainerModal.show();
    });

    const createTrainerForm = document.querySelector("#createTrainerForm");
    console.log(createTrainerForm);
    createTrainerForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        _clearErrorMessages();
        await _submitWithAjax(createTrainerForm);
    });

    // _submitWithAjax is a function that takes in the previously made createTrainerForm from _trainerModal
    // and uses that to creeate a url that includes the method of the form, as well as the action (create) + ajax.
    // If it works, will hide trainer and update table.

    async function _submitWithAjax(createTrainerForm) {
        const url = createTrainerForm.getAttribute('action') + "Ajax";
        const method = createTrainerForm.getAttribute('method');
        const formData = new FormData(createTrainerForm);
        const response = await fetch(url, {
            method: method, body: formData 
        });
        if (response.ok == false) {
            throw new Error("There was an HTTP error!");
        }
        const result = await response.json();
        if (result == "failed") {
            return;
        }
        createTrainerModal.hide();
        console.log(result.trainerId);
        _updateTrainerTable(result.trainerId);
    }

    // Simply to clear error messages
    
    function _clearErrorMessages() {
        let spans = document.querySelectorAll('span[data-valmsg-for]');
        spans.forEach(s => {
            s.textContent = "";
        });
    }
})();

// _updateTrainerTable is a function that takes the parameter of trainerId
// and allows the address to be fetched, awaiting the response. The response
// is whether or not the result is then appending to the trainer list.

async function _updateTrainerTable(trainerId) {
    const address = `${baseAddress}/trainer/trainerrow/${trainerId}`
    const response = await fetch(address);
    if (!response.ok) {
        throw new Error("There was an HTTP error getting the Trainer data.");
    }
    const result = await response.text();
    $("#tbody-trainer").append(result);
}