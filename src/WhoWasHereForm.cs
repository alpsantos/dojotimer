﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DojoTimer
{
    public partial class WhoWasHereForm : Form
    {
        Options options;
        public string Person1 { get { return Person1Input.Text; } }
        public string Person2 { get { return Person2Input.Text; } }

        public WhoWasHereForm(Options options)
        {
            InitializeComponent();
            this.options = options;
            SetLastParticipant(options);
            var autocomplete = GetAutoCompleteSource(options);

            SetupAutoComplete(Person1Input, autocomplete);
            SetupAutoComplete(Person2Input, autocomplete);
        }

        public static void SetupAutoComplete(TextBox textbox, AutoCompleteStringCollection autocomplete)
        {
            textbox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textbox.AutoCompleteCustomSource = autocomplete;
        }

        private static AutoCompleteStringCollection GetAutoCompleteSource(Options options)
        {
            var autocomplete = new AutoCompleteStringCollection();
            autocomplete.AddRange(options.Participants);
            return autocomplete;
        }

        private void SetLastParticipant(Options options)
        {
            if (options.Participants.Length > 0)
                Person2Input.Text = options.Participants[options.Participants.Length - 1];
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidatePerson()
        {
            bool result = !string.IsNullOrEmpty(Person1) && !string.IsNullOrEmpty(Person2);
            if (!result)
                MessageBox.Show(this, "Please, if you want to save, make sure you fill both boxes.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            return result;
        }

        private void OnlySaveButton_Click(object sender, EventArgs e)
        {
            if (ValidatePerson())
            {
                options.MarkFinish(false, Person1, Person2);
                options.Save();
                this.Close();
            }
        }

        private void SaveCommitButton_Click(object sender, EventArgs e)
        {
            if (ValidatePerson())
            {
                options.MarkFinish(true, Person1, Person2);
                options.Save();
                this.Close();
            }
        }
    }
}