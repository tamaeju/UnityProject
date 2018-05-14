using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour {
	[SerializeField]
	AudioClip[] SEs;
	[SerializeField]
	AudioClip menuSE;

	[SerializeField]
	AudioSource m_audiosource;
	public void playSEs (int sekind) {
		if (sekind < SEs.Length) {
			m_audiosource.PlayOneShot (SEs[sekind]);
		}
	}
	public void playMenuSE () {
		m_audiosource.PlayOneShot (menuSE);

	}
}