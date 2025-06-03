using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;
using System.Collections.Generic;

public class resetControls : MenuItems {
    protected override void Action() {
        eevee.inject.install(sys.data.config);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
